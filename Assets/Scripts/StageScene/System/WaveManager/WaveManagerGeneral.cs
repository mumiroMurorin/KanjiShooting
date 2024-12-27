using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

public class WaveManagerGeneral : WaveManager
{
    [SerializeField] QuestionFilter questionFilter;
    [SerializeField] KanjiObjectSpawner kanjiSpawner;
    [SerializeField] SerializeInterface<IQuestionSelector> questionSelector;
    [SerializeField] AudioClip bgm;

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

        foreach (IWaveStatus waveStatus in waveStatuses)
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