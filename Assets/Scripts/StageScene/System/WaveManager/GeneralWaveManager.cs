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
    /// ������
    /// </summary>
    protected override void AfterInitialize()
    {
        //�X�|�i�[�̏�����
        foreach (IWaveStatus w in waveStatuses)
        {
            w.Initialize(questionSelector.Value, kanjiSpawner);
        }
    }

    /// <summary>
    /// Wave�J�n
    /// </summary>
    public override void StartWave()
    {
        isWorking = true;
        Sound.SoundManager.Instance.PlayBGM(bgmType); //BGM�̍Đ�
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

        foreach (IWaveStatus waveStatus in waveStatuses)
        {
            waveStatus.SpawnEnemy(TimeRatio, GenerateEnemyInitializationData());
        }
    }

    protected override void AfterUpdate()
    {
        //�G���X�|�[��������
        if (intervalCount > spawnInterval)
        {
            intervalCount = 0;
            SpawnEnemy();
        }

        //Wave�I��
        if (time > maxTime)
        {
            //�Đ���~
            //Sound.SoundManager.Instance.StopBGM(true);
            EndOfWave();
        }
    }
}