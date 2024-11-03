using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

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
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] float maxTime = 30f;
    [SerializeField] float spawnInterval;
    [SerializeField] WaveStatus[] waveStatuses;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] QuestionFilter questionFilter;

    //WAVE�I�����̃R�[���o�b�N
    private Subject<Unit> onEndWaveSubject = new Subject<Unit>();
    public IObservable<Unit> OnEndWaveAsObservable => onEndWaveSubject;

    //WAVE�I����
    private Subject<Unit> onEndWaveFinishingSubject = new Subject<Unit>();
    public IObservable<Unit> OnEndWaveFinishingAsObservable => onEndWaveFinishingSubject;

    //�e��X�e�[�^�X
    float TimeRatio => time / maxTime;
    bool isWorking = false;
    float time;
    float intervalCount;
    Transform playerTransform;
    KanjiObjectSpawner kanjiSpawner;
    IQuestionSelector questionSelector;
    Vector3[] spawnPositions;

    /// <summary>
    /// ������
    /// </summary>
    public void Initialize(Transform playerTransform, KanjiObjectSpawner kanjiSpawner, IQuestionSelector questionSelector)
    {
        time = 0;
        intervalCount = float.MaxValue;

        this.playerTransform = playerTransform;
        this.kanjiSpawner = kanjiSpawner;
        this.questionSelector = questionSelector;

        //�X�|�[���|�C���g�̐ݒ�
        spawnPositions = new Vector3[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPositions[i] = spawnPoints[i].position;
        }

        //�X�|�i�[�̏�����
        foreach (WaveStatus w in waveStatuses)
        {
            w.Initialize();
        }
    }

    /// <summary>
    /// Wave�J�n
    /// </summary>
    public void StartWave() 
    {
        isWorking = true;
    }

    /// <summary>
    /// Wave�I��
    /// </summary>
    public void FinishWave()
    {
        isWorking = false;
        EndOfWaveFinish();
    }

    /// <summary>
    /// ���Ԃ̍X�V�ɔ�������
    /// </summary>
    private void Update()
    {
        if (!isWorking) { return; }

        //���Ԃ̉��Z
        time += Time.deltaTime;
        intervalCount += Time.deltaTime;

        //�G���X�|�[��������
        if (intervalCount > spawnInterval) 
        {
            intervalCount = 0;
            SpawnEnemy();
        }

        //Wave�I��
        if (time > maxTime)
        {
            EndOfWave();
        }
    }

    /// <summary>
    /// �G�̃X�|�[��
    /// </summary>
    private void SpawnEnemy()
    {
        if (questionSelector == null) { return; }

        for (int i = 0; i < waveStatuses.Length; i++)
        {
            //�E�F�[�u���̓G�̐���
            WaveStatus wave = waveStatuses[i];
            for (int spawnNum = 0; spawnNum < wave.GetSpawnNum(TimeRatio); spawnNum++)
            {
                //���̑I��
                QuestionData data = questionSelector.GetQuestionData(questionFilter);
                //�����̐���
                GameObject kanjiObject = kanjiSpawner.SpawnKanji(data);
                //�X�|�[��
                wave.SpawnEnemy(GenerateEnemyInitializationData(kanjiObject, data));
            }
        }
    }

    /// <summary>
    /// WAVE�I����
    /// </summary>
    private void EndOfWave()
    {
        //�R�[���o�b�N����
        onEndWaveSubject.OnNext(Unit.Default);
    }

    /// <summary>
    /// WAVE�I������������
    /// </summary>
    private void EndOfWaveFinish()
    {
        //�R�[���o�b�N����
        onEndWaveFinishingSubject.OnNext(Unit.Default);
    }

    private EnemyInitializationData GenerateEnemyInitializationData(GameObject kanjiObject, QuestionData questionData)
    {
        EnemyInitializationData enemyInitializationData = new EnemyInitializationData
        {
            spawnPoint = SelectionSpawnpoint(),
            target = playerTransform,
            kanjiObject = kanjiObject,
            questionData = questionData
        };

        return enemyInitializationData;
    }

    private Transform SelectionSpawnpoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
    }
}