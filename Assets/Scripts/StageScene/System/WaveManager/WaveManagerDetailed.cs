using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

public class WaveManagerDetailed : WaveManager
{
    [SerializeField] float spawnInterval;
    [SerializeField] KanjiObjectSpawner kanjiSpawner;
    [SerializeField] Sound.BGM_Type bgmType;
    [SerializeReference, SubclassSelector] IQuestionSelector questionSelector;
    [SerializeReference, SubclassSelector] IWaveStatus[] waveStatuses;

    /// <summary>
    /// 初期化
    /// </summary>
    protected override void AfterInitialize()
    {
        questionSelector.Initialize();

        //スポナーの初期化
        foreach (IWaveStatus w in waveStatuses)
        {
            w.Initialize(questionSelector, kanjiSpawner);
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
        foreach (IWaveStatus wave in waveStatuses) { wave.DespawnEnemy(); } 

        EndOfWaveFinish();
    }

    protected override void SpawnEnemy()
    {
        if (questionSelector == null) { return; }

        foreach(IWaveStatus waveStatus in waveStatuses)
        {
            waveStatus.SpawnEnemy(TimeRatio, GenerateEnemyInitializationData());
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
}

[System.Serializable]
class WaveStatusGeneral : IWaveStatus
{
    [SerializeField] EnemySpawner spawner;
    [SerializeField] AnimationCurve curve;
    [SerializeField] QuestionFilter filter;

    IQuestionSelector questionSelector;
    KanjiObjectSpawner kanjiSpawner;

    public void Initialize(IQuestionSelector qSelector, KanjiObjectSpawner kSpawner)
    {
        questionSelector = qSelector;
        kanjiSpawner = kSpawner;

        spawner.Initialize();
    }

    public void SpawnEnemy(float timeRatio, EnemyInitializationData enemyInitializationData)
    {
        for (int spawnNum = 0; spawnNum < curve.Evaluate(timeRatio); spawnNum++)
        {
            //問題の選定
            QuestionData data = questionSelector.GetQuestionData(filter);
            enemyInitializationData.questionData = data;
            //漢字の生成
            enemyInitializationData.kanjiObject = kanjiSpawner.SpawnKanji(data);
            //スポーン
            spawner.SpawnEnemy(enemyInitializationData);
        }
    }

    public void DespawnEnemy()
    {
        spawner.DespawnEnemy();
    }
}