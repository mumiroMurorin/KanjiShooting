using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

[System.Serializable]
class WaveStatusGeneral : IWaveStatus
{
    [SerializeField] string statusName;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] QuestionFilter filter;
    [SerializeField] SerializeInterface<ISpawnpointSelector> spawnSelector;
    [SerializeField] AnimationCurve curve;

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