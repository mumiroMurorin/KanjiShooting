using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

public class WaveManagerDetailed : WaveManager
{
    [SerializeField] KanjiObjectSpawner kanjiSpawner;
    [SerializeReference, SubclassSelector] IQuestionSelector questionSelector;
    [SerializeField] AudioClip bgm;

    /// <summary>
    /// ������
    /// </summary>
    protected override void AfterInitialize()
    {
        questionSelector.Initialize();

        //�X�|�i�[�̏�����
        foreach (IWaveStatus w in waveStatuses)
        {
            w.Initialize(questionSelector, kanjiSpawner);
        }
    }

    /// <summary>
    /// Wave�J�n
    /// </summary>
    public override void StartWave()
    {
        isWorking = true;
        Sound.SoundManager.Instance.PlayBGM(bgm); //BGM�̍Đ�
    }

    /// <summary>
    /// Wave�I��
    /// </summary>
    public override void FinishWave()
    {
        isWorking = false;

        //BGM�̒�~
        Sound.SoundManager.Instance.StopBGM(true);
        //�S�Ă̓G���f�X�|�[��������
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
        SpawnEnemy();

        //Wave�I��
        if (time > maxTime)
        {
            //�Đ���~
            //Sound.SoundManager.Instance.StopBGM(true);
            EndOfWave();
        }
    }
}