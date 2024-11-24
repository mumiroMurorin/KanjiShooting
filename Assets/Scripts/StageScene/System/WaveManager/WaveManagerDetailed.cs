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