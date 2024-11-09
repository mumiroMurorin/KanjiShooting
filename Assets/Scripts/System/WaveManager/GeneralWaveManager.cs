using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

public class GeneralWaveManager : WaveManager
{
    [SerializeField] float spawnInterval;

    [SerializeReference, SubclassSelector] IWaveStatus[] waveStatuses;
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
        foreach (IWaveStatus w in waveStatuses)
        {
            w.Initialize(questionSelector.Value, kanjiSpawner);
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

        foreach (IWaveStatus waveStatus in waveStatuses)
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