using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Kanji;

public struct EnemyInitializationData
{
    public Transform spawnPoint;
    public Transform target;
    public GameObject kanjiObject;
    public QuestionData questionData;
    public ScoreHolder scoreHolder;
}

public abstract class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected GameObject EnemyObject;
    List<IEnemy> spawnEnemyList;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="target"></param>
    public void Initialize()
    {
        spawnEnemyList = new List<IEnemy>();
    }

    /// <summary>
    /// 敵の出現
    /// </summary>
    /// <param name="pos"></param>
    public void SpawnEnemy(EnemyInitializationData enemyInitializationData)
    {
        IEnemy enemy = Instantiate(EnemyObject, enemyInitializationData.spawnPoint.position, enemyInitializationData.spawnPoint.rotation).GetComponent<IEnemy>();
        spawnEnemyList.Add(enemy);
        AfterSpawn(enemy, enemyInitializationData);
    }

    protected abstract void AfterSpawn(IEnemy enemy, EnemyInitializationData enemyInitializationData);

    /// <summary>
    /// 出現した全ての敵をデスポーンさせる
    /// </summary>
    public void DespawnEnemy()
    {
        //苦渋の対策、なぜかただのnullだと正しく判別してくれない…
        spawnEnemyList = spawnEnemyList.Where(e => e.ToString() != "null").ToList();
        foreach (IEnemy enemy in spawnEnemyList)
        {
            enemy.Despawn();
        }
    }
}
