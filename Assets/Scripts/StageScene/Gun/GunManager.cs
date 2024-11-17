using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class GunManager : MonoBehaviour
{    
    IYomiganaSpawner yomiganaSpawner;
    IBulletShooter bulletShooter;
    IBulletSpawner bulletSpawner;

    protected IYomiganaSpawner YomiganaSpawner { set { yomiganaSpawner = value; } }
    protected IBulletShooter BulletShooter { set { bulletShooter = value; } }
    protected IBulletSpawner BulletSpawner { set { bulletSpawner = value; } }

    protected BulletController bullet;
    protected ReactiveProperty<float> currentReloadValue = new ReactiveProperty<float>(1f);

    GameObject hiraganaParent;

    public IReadOnlyReactiveProperty<float> ReloadValue
    {
        get { return currentReloadValue; }
    }

    private void Start()
    {
        Initialize();
        SpawnHiraganaParent();
    }

    protected abstract void Initialize();

    /// <summary>
    /// 一時的にひらがなオブジェクトを乗っける親を作る
    /// </summary>
    private void SpawnHiraganaParent()
    {
        hiraganaParent = new GameObject("HiraganaParent");
        SetParent(hiraganaParent.transform, this.transform);

        yomiganaSpawner.BulletTransform = hiraganaParent.transform;
    }

    /// <summary>
    /// 弾の生成
    /// </summary>
    private void SpawnBullet()
    {
        bullet = bulletSpawner.Spawn();
        bulletShooter.Bullet = bullet;
    }

    /// <summary>
    /// Transform.SetParent()に位置と角度の同期処理を追加
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    private void SetParent(Transform child, Transform parent)
    {
        child.transform.SetParent(parent);
        child.transform.position = parent.position;
        child.transform.eulerAngles = parent.eulerAngles;
    }

    /// <summary>
    /// 弾の発射
    /// </summary>
    /// <returns></returns>
    public virtual void Shoot()
    {
        SpawnBullet();

        // 予め用意しておいた平仮名オブジェクトを生成した弾に乗っける
        SetParent(hiraganaParent.transform, bullet.transform);

        BulletController b = bulletShooter.Shoot();
        b.Yomigana = yomiganaSpawner.Answer.Value;
        yomiganaSpawner.OnShoot();

        // 再度ひらがなオブジェクトの親を生成
        SpawnHiraganaParent();
    }
}
