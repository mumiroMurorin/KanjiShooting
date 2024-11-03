using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyInitializationData
{
    public Transform spawnPoint;
    public Transform target;
    public GameObject kanjiObject;
    public QuestionData questionData;
}

public abstract class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected GameObject EnemyObject;
     
    /// <summary>
    /// èâä˙âª
    /// </summary>
    /// <param name="target"></param>
    public void Initialize()
    {
        
    }

    /// <summary>
    /// ìGÇÃèoåª
    /// </summary>
    /// <param name="pos"></param>
    public void SpawnEnemy(EnemyInitializationData enemyInitializationData)
    {
        IEnemy enemy = Instantiate(EnemyObject, enemyInitializationData.spawnPoint.position, enemyInitializationData.spawnPoint.rotation).GetComponent<IEnemy>();
        AfterSpawn(enemy, enemyInitializationData);
    }

    protected abstract void AfterSpawn(IEnemy enemy, EnemyInitializationData enemyInitializationData);
}
