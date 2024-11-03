using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class GunManager : MonoBehaviour
{    
    [SerializeField] protected SerializeInterface<IYomiganaSpawner> yomiganaSpawner;
    [SerializeField] protected SerializeInterface<IBulletShooter> bulletShooter;
    [SerializeField] protected SerializeInterface<IBulletSpawner> bulletSpawner;

    protected BulletController bullet;
    protected ReactiveProperty<float> currentReloadValue = new ReactiveProperty<float>(1f);

    public IReadOnlyReactiveProperty<float> ReloadValue
    {
        get { return currentReloadValue; }
    }

    private void Start()
    {
        Initialize();
    }
    
    protected virtual void Initialize()
    {
        Spawn();
    }

    /// <summary>
    /// íeÇÃê∂ê¨
    /// </summary>
    protected void Spawn()
    {
        bullet = bulletSpawner.Value.Spawn();
        bulletShooter.Value.Bullet = bullet;
        yomiganaSpawner.Value.BulletTransform = bullet.transform;
    }

    /// <summary>
    /// íeÇÃî≠éÀ
    /// </summary>
    /// <returns></returns>
    public virtual void Shoot()
    {
        if (!bullet) { return; }

        BulletController b = bulletShooter.Value.Shoot();
        b.Yomigana = yomiganaSpawner.Value.Answer.Value;
        yomiganaSpawner.Value.OnShoot();
        Spawn();
    }
}
