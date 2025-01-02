using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BirdEnemySpawner : EnemySpawner 
{
    //[SerializeField] GameObject spawnEffect;

    protected override void AfterSpawn(IEnemy enemy, EnemyInitializationData enemyInitializationData)
    {
        //Instantiate(spawnEffect, enemyInitializationData.spawnPoint.position, enemyInitializationData.spawnPoint.rotation);
        enemy.Initialize(enemyInitializationData);
    }
}
