using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.IO;
using System;
using Kanji;
using System.Linq;

public class QuestionSelectorFromCSV : IQuestionSelector
{
    const int FIRSTQUESTION_COLUMN = 1; //漢字が何行目から始まるか
    const int KANJI_RAW = 0;
    const int FURIGANA_RAW = 1;
    const int ANSWERS_RAW = 2;
    const int LEVEL_RAW = 3;
    const int RADICAL_RAW = 4;
    const int OTHERTAG_RAW = 5;

    [SerializeField] QuestionFilter[] questionFilters;

    class QuestionDataRandom
    {
        public List<QuestionData> originalData;
        public List<QuestionData> randomData;

        /// <summary>
        /// ランダムなデータを返す
        /// 連続して同じデータは返さない
        /// </summary>
        /// <returns></returns>
        public QuestionData GetQuestionData()
        {
            if (randomData == null) { Shuffle(); }
            if (randomData.Count == 0) { Shuffle(); }

            QuestionData data = randomData[0];
            randomData.RemoveAt(0);
            return data;
        }

        /// <summary>
        /// ランダムに並び替える
        /// </summary>
        private void Shuffle()
        {
            randomData = new List<QuestionData>();
            randomData = originalData.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }

    CancellationTokenSource cts;
    List<QuestionData> questionDatas;
    Dictionary<QuestionFilter, QuestionDataRandom> questionListDictionary;

    /// <summary>
    /// 非同期処理でファイルを読み込み
    /// </summary>
    public async void Initialize()
    {
        cts = new CancellationTokenSource();

        // CSVのロード
        List<string[]> kanjiData = await LoadCsvAsync(StageManager.Instance.KanjiCSV, cts.Token);

        // 問題データへの変換
        questionDatas = await ConvertQuestionDataListAsync(kanjiData, cts.Token);

        // 問題データの抜粋
        questionListDictionary = new Dictionary<QuestionFilter, QuestionDataRandom>();
        foreach (QuestionFilter filter in questionFilters)
        {
            var list = await PickUpQuestionData(filter, questionDatas, cts.Token);
            questionListDictionary.Add(filter, new QuestionDataRandom { originalData = list });
        }
    }

    /// <summary>
    /// 問題の抜粋
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public QuestionData GetQuestionData(QuestionFilter filter)
    {
        // 問題データがあるか確認
        if (!questionListDictionary.TryGetValue(filter, out QuestionDataRandom datas)) 
        {
            Debug.Log("問題データがありません");
            return new QuestionData();
        }

        return datas.GetQuestionData();
    }

    /// <summary>
    /// CSVを非同期でList<string[]>に
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask<List<string[]>> LoadCsvAsync(TextAsset csvFile, CancellationToken token)
    {
        if (csvFile == null)
        {
            Debug.LogError("【System】CSVファイルが設定されていません");
            return null;
        }

        List<string[]> csvData = new List<string[]>();

        // CSVを非同期で読み込む
        using (StringReader reader = new StringReader(csvFile.text))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                // CSVの1行をカンマ区切りで分割して配列にする
                string[] rowData = line.Split(',');
                csvData.Add(rowData);
                if (token.IsCancellationRequested) { Debug.Log("cancell"); return null; }
            }
        }

        return csvData;
    }

    /// <summary>
    /// List<string[]> → 問題データ
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask<List<QuestionData>> ConvertQuestionDataListAsync(List<string[]> data, CancellationToken token)
    {
        List<QuestionData> list = new List<QuestionData>();

        // 一列(一問)ずつ抽出して変換
        for(int i = 0; i < data.Count; i++)
        {
            if (i < FIRSTQUESTION_COLUMN) { continue; }
            if (token.IsCancellationRequested) { Debug.Log("cancell"); return null; }
            list.Add(ConvertQuestionData(data[i]));
        }

        await UniTask.WaitUntil(() => list != null, cancellationToken: token);
        return list;
    }

    /// <summary>
    /// string[] → QuestionData
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    private QuestionData ConvertQuestionData(string[] array)
    {
        QuestionData data = new QuestionData();
        if (array.Length > KANJI_RAW) { data.kanji = array[KANJI_RAW]; }
        if (array.Length > FURIGANA_RAW) { data.furigana = array[FURIGANA_RAW]; }
        if (array.Length > ANSWERS_RAW) { data.answers = array[ANSWERS_RAW].Split("、"); }
        if (array.Length > LEVEL_RAW) {
            try
            {
                data.level = Kanji.Convert.StringToLevel(array[LEVEL_RAW]);
            }
            catch (ArgumentException ex)
            {
                Debug.LogWarning($"エラー: {ex.Message}");
            }
        }
        if (array.Length > RADICAL_RAW) {
            try
            {
                data.radical = Kanji.Convert.StringToRadical(array[RADICAL_RAW]);
            }
            catch (ArgumentException ex)
            {
                Debug.LogWarning($"エラー: {ex.Message}");
            }
        }
        if (array.Length > OTHERTAG_RAW) {
            try
            {
                data.otherTag = Kanji.Convert.StringToOtherTag(array[OTHERTAG_RAW]);
            }
            catch (ArgumentException ex)
            {
                Debug.LogWarning($"エラー: {ex.Message}");
            }
        }

        //Debug.Log($"Kanji: {data.kanji}, Answers: {string.Join(", ", data.answers)}, Level: {data.level}, Radical: {data.radical}, OtherTag: {data.otherTag}");
        return data;
    }

    /// <summary>
    /// クエスチョンデータの抜粋
    /// </summary>
    /// <returns></returns>
    private async UniTask<List<QuestionData>> PickUpQuestionData(QuestionFilter filter, List<QuestionData> datas, CancellationToken token)
    {
        List<QuestionData> lists = new List<QuestionData>();
        foreach (QuestionData data in datas)
        {
            if (token.IsCancellationRequested) { Debug.Log("cancell"); return null; }
            if (!filter.Compare(data)) { continue; }
            lists.Add(data);
        }

        await UniTask.WaitUntil(() => lists != null, cancellationToken: token);
        return lists;
    }

    private void OnDestroy()
    {
        // キャンセルしてリソースを解放
        cts?.Cancel();
        cts?.Dispose();
    }
}
