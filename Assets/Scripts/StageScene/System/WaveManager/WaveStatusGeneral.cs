using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

[System.Serializable]
class WaveStatusGeneral : IWaveStatus
{
    [SerializeField] string statusName;
    [SerializeField] float interval;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] QuestionFilter filter;
    [SerializeField] SerializeInterface<ISpawnpointSelector> spawnSelector;
    [SerializeField] AnimationCurve curve;

    IQuestionSelector questionSelector;
    KanjiObjectSpawner kanjiSpawner;

    float intervalCount;

    public void Initialize(IQuestionSelector qSelector, KanjiObjectSpawner kSpawner)
    {
        questionSelector = qSelector;
        kanjiSpawner = kSpawner;
        intervalCount = float.MaxValue;

        spawner.Initialize();
    }

    public void CountTime(float addTime)
    {
        intervalCount += addTime;
    }

    public void SpawnEnemy(float timeRatio, EnemyInitializationData enemyInitializationData)
    {
        // �J�E���g�����܂��ĂȂ��Ƃ��߂�
        if (intervalCount < interval) { return; }

        intervalCount = 0;

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