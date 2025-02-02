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
        // カウントがたまってないとき戻す
        if (intervalCount < interval) { return; }

        intervalCount = 0;

        for (int spawnNum = 0; spawnNum < curve.Evaluate(timeRatio); spawnNum++)
        {
            // 問題の選定
            QuestionData data = questionSelector.GetQuestionData(filter);
            enemyInitializationData.questionData = data;
            // 漢字の生成
            enemyInitializationData.kanjiObject = kanjiSpawner.SpawnKanji(data);
            // スポーンポイントの設定
            enemyInitializationData.spawnPoint = spawnSelector.Value.GetSpawnPoint();

            // スポーン
            spawner.SpawnEnemy(enemyInitializationData);
        }
    }

    public void DespawnEnemy()
    {
        spawner.DespawnEnemy();
    }
}