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
            // 抽選
            if(probabilityCurve.Evaluate(timeRatio) < Random.Range(0f, 1f)) { continue; }

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