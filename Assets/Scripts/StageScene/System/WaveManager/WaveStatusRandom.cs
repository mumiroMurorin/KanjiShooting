using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kanji;

[System.Serializable]
class WaveStatusRandom : IWaveStatus
{
    [System.Serializable]
    class EnemyProbability
    {
        [SerializeField] EnemySpawner spawner;
        [SerializeField] float weight;

        public float Weight { get { return weight; } }
        public EnemySpawner Spawner { get { return spawner; } }
    }

    [SerializeField] string statusName;
    [SerializeField] float interval;
    [SerializeField] EnemyProbability[] enemyPlobabilities;
    [SerializeField] QuestionFilter filter;
    [SerializeField] SerializeInterface<ISpawnpointSelector> spawnSelector;
    [SerializeField] AnimationCurve amountCurve;

    IQuestionSelector questionSelector;
    KanjiObjectSpawner kanjiSpawner;
    float weightSum;
    float intervalCount;

    public void Initialize(IQuestionSelector qSelector, KanjiObjectSpawner kSpawner)
    {
        questionSelector = qSelector;
        kanjiSpawner = kSpawner;

        foreach(EnemyProbability e in enemyPlobabilities)
        {
            // 重みの合計を算出
            weightSum += e.Weight;
            e.Spawner.Initialize();
        }

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

        for (int spawnNum = 0; spawnNum < amountCurve.Evaluate(timeRatio); spawnNum++)
        {
            // 抽選
            EnemySpawner spawner = null;
            float count = 0;
            float random = Random.Range(0f, 1f);

            foreach(EnemyProbability e in enemyPlobabilities)
            {
                // 正規化した値を足し合わせる
                count += e.Weight / weightSum; 
                if (count >= random) { spawner = e.Spawner; break; }
            }
            
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
        foreach (EnemyProbability e in enemyPlobabilities)
        {
            e.Spawner.DespawnEnemy();
        }
    }
}