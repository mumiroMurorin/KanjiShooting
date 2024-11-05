using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class WaveStatus
{
    [SerializeField] EnemySpawner spawner;
    [SerializeField] AnimationCurve curve;

    public float GetSpawnNum(float timeRatio) { return curve.Evaluate(timeRatio); }

    public void Initialize()
    {
        spawner.Initialize();
    }

    public void SpawnEnemy(EnemyInitializationData enemyInitializationData)
    {
        spawner.SpawnEnemy(enemyInitializationData);
    }

    public void DespawnEnemy()
    {
        spawner.DespawnEnemy();
    }
}

public class GeneralStageManager : WaveManager
{
    [SerializeField] float spawnInterval;

    [SerializeField] WaveStatus[] waveStatuses;
    [SerializeField] QuestionFilter questionFilter;
    [SerializeField] KanjiObjectSpawner kanjiSpawner;
    [SerializeField] SerializeInterface<IQuestionSelector> questionSelector;
    [SerializeField] Sound.BGM_Type bgmType;

    /// <summary>
    /// 初期化
    /// </summary>
    protected override void AfterInitialize()
    {
        //スポナーの初期化
        foreach (WaveStatus w in waveStatuses)
        {
            w.Initialize();
        }
    }

    /// <summary>
    /// Wave開始
    /// </summary>
    public override void StartWave()
    {
        isWorking = true;
        Sound.SoundManager.Instance.PlayBGM(bgmType); //BGMの再生
    }

    /// <summary>
    /// Wave終了
    /// </summary>
    public override void FinishWave()
    {
        isWorking = false;

        //BGMの停止
        Sound.SoundManager.Instance.StopBGM(true);
        //全ての敵をデスポーンさせる
        foreach (WaveStatus wave in waveStatuses) { wave.DespawnEnemy(); } 

        EndOfWaveFinish();
    }

    protected override void SpawnEnemy()
    {
        if (questionSelector == null) { return; }

        for (int i = 0; i < waveStatuses.Length; i++)
        {
            //ウェーブ内の敵の数毎
            WaveStatus wave = waveStatuses[i];
            for (int spawnNum = 0; spawnNum < wave.GetSpawnNum(TimeRatio); spawnNum++)
            {
                //問題の選定
                QuestionData data = questionSelector.Value.GetQuestionData(questionFilter);
                //漢字の生成
                GameObject kanjiObject = kanjiSpawner.SpawnKanji(data);
                //スポーン
                wave.SpawnEnemy(GenerateEnemyInitializationData(kanjiObject, data));
            }
        }
    }

    protected override void AfterUpdate()
    {
        //敵をスポーンさせる
        if (intervalCount > spawnInterval)
        {
            intervalCount = 0;
            SpawnEnemy();
        }

        //Wave終了
        if (time > maxTime)
        {
            //再生停止
            //Sound.SoundManager.Instance.StopBGM(true);
            EndOfWave();
        }
    }

    /// <summary>
    /// エネミーの初期化データを生成
    /// </summary>
    /// <param name="kanjiObject"></param>
    /// <param name="questionData"></param>
    /// <returns></returns>
    private EnemyInitializationData GenerateEnemyInitializationData(GameObject kanjiObject, QuestionData questionData)
    {
        EnemyInitializationData enemyInitializationData = new EnemyInitializationData
        {
            spawnPoint = SelectionSpawnpoint(),
            target = PlayerTransform,
            kanjiObject = kanjiObject,
            questionData = questionData
        };

        return enemyInitializationData;
    }

    /// <summary>
    /// スポーンポイントのランダム選出 (別クラスにすべきだが一旦)
    /// </summary>
    /// <returns></returns>
    private Transform SelectionSpawnpoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }
}
