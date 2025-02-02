using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBulletSpawner : MonoBehaviour, IBulletSpawner
{
    [SerializeField] Transform parent;
    [SerializeField] GameObject bulletPrefab;

    /// <summary>
    /// 弾の生成
    /// </summary>
    /// <returns></returns>
    public BulletController Spawn()
    {
        if (!bulletPrefab) { Debug.LogError("弾がセットされてないよ"); return null; }

        return Instantiate(bulletPrefab, parent).GetComponent<BulletController>();
    }
}
