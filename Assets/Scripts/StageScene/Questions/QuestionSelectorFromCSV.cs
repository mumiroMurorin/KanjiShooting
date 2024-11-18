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
    const int FIRSTQUESTION_COLUMN = 1; //���������s�ڂ���n�܂邩
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
        /// �����_���ȃf�[�^��Ԃ�
        /// �A�����ē����f�[�^�͕Ԃ��Ȃ�
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
        /// �����_���ɕ��ёւ���
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
    /// �񓯊������Ńt�@�C����ǂݍ���
    /// </summary>
    public async void Initialize()
    {
        cts = new CancellationTokenSource();

        // CSV�̃��[�h
        List<string[]> kanjiData = await LoadCsvAsync(StageManager.Instance.KanjiCSV, cts.Token);

        // ���f�[�^�ւ̕ϊ�
        questionDatas = await ConvertQuestionDataListAsync(kanjiData, cts.Token);

        // ���f�[�^�̔���
        questionListDictionary = new Dictionary<QuestionFilter, QuestionDataRandom>();
        foreach (QuestionFilter filter in questionFilters)
        {
            var list = await PickUpQuestionData(filter, questionDatas, cts.Token);
            questionListDictionary.Add(filter, new QuestionDataRandom { originalData = list });
        }
    }

    /// <summary>
    /// ���̔���
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public QuestionData GetQuestionData(QuestionFilter filter)
    {
        // ���f�[�^�����邩�m�F
        if (!questionListDictionary.TryGetValue(filter, out QuestionDataRandom datas)) 
        {
            Debug.Log("���f�[�^������܂���");
            return new QuestionData();
        }

        return datas.GetQuestionData();
    }

    /// <summary>
    /// CSV��񓯊���List<string[]>��
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask<List<string[]>> LoadCsvAsync(TextAsset csvFile, CancellationToken token)
    {
        if (csvFile == null)
        {
            Debug.LogError("�ySystem�zCSV�t�@�C�����ݒ肳��Ă��܂���");
            return null;
        }

        List<string[]> csvData = new List<string[]>();

        // CSV��񓯊��œǂݍ���
        using (StringReader reader = new StringReader(csvFile.text))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                // CSV��1�s���J���}��؂�ŕ������Ĕz��ɂ���
                string[] rowData = line.Split(',');
                csvData.Add(rowData);
                if (token.IsCancellationRequested) { Debug.Log("cancell"); return null; }
            }
        }

        return csvData;
    }

    /// <summary>
    /// List<string[]> �� ���f�[�^
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask<List<QuestionData>> ConvertQuestionDataListAsync(List<string[]> data, CancellationToken token)
    {
        List<QuestionData> list = new List<QuestionData>();

        // ���(���)�����o���ĕϊ�
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
    /// string[] �� QuestionData
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    private QuestionData ConvertQuestionData(string[] array)
    {
        QuestionData data = new QuestionData();
        if (array.Length > KANJI_RAW) { data.kanji = array[KANJI_RAW]; }
        if (array.Length > FURIGANA_RAW) { data.furigana = array[FURIGANA_RAW]; }
        if (array.Length > ANSWERS_RAW) { data.answers = array[ANSWERS_RAW].Split("�A"); }
        if (array.Length > LEVEL_RAW) {
            try
            {
                data.level = Kanji.Convert.StringToLevel(array[LEVEL_RAW]);
            }
            catch (ArgumentException ex)
            {
                Debug.LogWarning($"�G���[: {ex.Message}");
            }
        }
        if (array.Length > RADICAL_RAW) {
            try
            {
                data.radical = Kanji.Convert.StringToRadical(array[RADICAL_RAW]);
            }
            catch (ArgumentException ex)
            {
                Debug.LogWarning($"�G���[: {ex.Message}");
            }
        }
        if (array.Length > OTHERTAG_RAW) {
            try
            {
                data.otherTag = Kanji.Convert.StringToOtherTag(array[OTHERTAG_RAW]);
            }
            catch (ArgumentException ex)
            {
                Debug.LogWarning($"�G���[: {ex.Message}");
            }
        }

        //Debug.Log($"Kanji: {data.kanji}, Answers: {string.Join(", ", data.answers)}, Level: {data.level}, Radical: {data.radical}, OtherTag: {data.otherTag}");
        return data;
    }

    /// <summary>
    /// �N�G�X�`�����f�[�^�̔���
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
        // �L�����Z�����ă��\�[�X�����
        cts?.Cancel();
        cts?.Dispose();
    }
}
