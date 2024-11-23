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
    /// ������
    /// </summary>
    /// <param name="target"></param>
    public void Initialize()
    {
        spawnEnemyList = new List<IEnemy>();
    }

    /// <summary>
    /// �G�̏o��
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
    /// �o�������S�Ă̓G���f�X�|�[��������
    /// </summary>
    public void DespawnEnemy()
    {
        //��a�̑΍�A�Ȃ���������null���Ɛ��������ʂ��Ă���Ȃ��c
        spawnEnemyList = spawnEnemyList.Where(e => e.ToString() != "null").ToList();
        foreach (IEnemy enemy in spawnEnemyList)
        {
            enemy.Despawn();
        }
    }
}
