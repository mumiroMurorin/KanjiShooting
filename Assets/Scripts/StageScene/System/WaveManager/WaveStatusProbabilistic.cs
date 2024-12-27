using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

[System.Serializable]
class WaveStatusProbabilistic : IWaveStatus
{
    [SerializeField] string statusName;
    [SerializeField] float interval;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] QuestionFilter filter;
    [SerializeField] SerializeInterface<ISpawnpointSelector> spawnSelector;
    [SerializeField] AnimationCurve amountCurve;
    [SerializeField] AnimationCurve probabilityCurve;

    IQuestionSelector questionSelector;
    KanjiObjectSpawner kanjiSpawner;
    float intervalCount;


    public void Initialize(IQuestionSelector qSelector, KanjiObjectSpawner kSpawner)
    {
        questionSelector = qSelector;
        kanjiSpawner = kSpawner;

        spawner.Initialize();
    }

    public void CountTime(float addTime)
    {
        intervalCount += addTime;
    }

    public void SpawnEnemy(float timeRatio, EnemyInitializationData enemyInitializationData)
    {
        for (int spawnNum = 0; spawnNum < amountCurve.Evaluate(timeRatio); spawnNum++)
        {
            // ���I
            if(probabilityCurve.Evaluate(timeRatio) < Random.Range(0f, 1f)) { continue; }

            // ���̑I��
            QuestionData data = questionSelector.GetQuestionData(filter);
            enemyInitializationData.questionData = data;
            // �����̐���
            enemyInitializationData.kanjiObject = kanjiSpawner.SpawnKanji(data);
            // �X�|�[���|�C���g�̐ݒ�
            enemyInitializationData.spawnPoint = spawnSelector.Value.GetSpawnPoint();

            // �X�|�[��
            spawner.SpawnEnemy(enemyInitializationData);
        }
    }

    public void DespawnEnemy()
    {
        spawner.DespawnEnemy();
    }
}