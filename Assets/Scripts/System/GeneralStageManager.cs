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
    /// ������
    /// </summary>
    protected override void AfterInitialize()
    {
        //�X�|�i�[�̏�����
        foreach (WaveStatus w in waveStatuses)
        {
            w.Initialize();
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
        foreach (WaveStatus wave in waveStatuses) { wave.DespawnEnemy(); } 

        EndOfWaveFinish();
    }

    protected override void SpawnEnemy()
    {
        if (questionSelector == null) { return; }

        for (int i = 0; i < waveStatuses.Length; i++)
        {
            //�E�F�[�u���̓G�̐���
            WaveStatus wave = waveStatuses[i];
            for (int spawnNum = 0; spawnNum < wave.GetSpawnNum(TimeRatio); spawnNum++)
            {
                //���̑I��
                QuestionData data = questionSelector.Value.GetQuestionData(questionFilter);
                //�����̐���
                GameObject kanjiObject = kanjiSpawner.SpawnKanji(data);
                //�X�|�[��
                wave.SpawnEnemy(GenerateEnemyInitializationData(kanjiObject, data));
            }
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

    /// <summary>
    /// �G�l�~�[�̏������f�[�^�𐶐�
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
    /// �X�|�[���|�C���g�̃����_���I�o (�ʃN���X�ɂ��ׂ�������U)
    /// </summary>
    /// <returns></returns>
    private Transform SelectionSpawnpoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }
}
