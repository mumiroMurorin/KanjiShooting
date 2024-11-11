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

        foreach(IWaveStatus waveStatus in waveStatuses)
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
            //���̑I��
            QuestionData data = questionSelector.GetQuestionData(filter);
            enemyInitializationData.questionData = data;
            //�����̐���
            enemyInitializationData.kanjiObject = kanjiSpawner.SpawnKanji(data);
            //�X�|�[��
            spawner.SpawnEnemy(enemyInitializationData);
        }
    }

    public void DespawnEnemy()
    {
        spawner.DespawnEnemy();
    }
}