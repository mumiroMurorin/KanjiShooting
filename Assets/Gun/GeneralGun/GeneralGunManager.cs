using System;
using UnityEngine;

public class GeneralGunManager : GunManager
{
    [Header("復活間隔(秒)")]
    [SerializeField] float reviveInterval;
    [SerializeField] SerializeInterface<IJapaneseInputHolder> inputHolder;

    [SerializeField] SerializeInterface<IBulletSpawner> bulletSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> bulletShooter;
    [SerializeField] SerializeInterface<IYomiganaSpawner> yomiganaSpawner;

    protected override void Initialize()
    {
        YomiganaSpawner = yomiganaSpawner.Value;
        BulletSpawner = bulletSpawner.Value;
        BulletShooter = bulletShooter.Value;
    }

    public override void Shoot()
    {
        if (currentReloadValue.Value < 1f) { return; }   // 補充されてないとき返す

        base.Shoot();

        // リロード
        inputHolder.Value.ClearInput();
        currentReloadValue.Value = 0;
    }

    // リロード
    private void Update()
    {
        if(currentReloadValue.Value >= 1f) { return; }
        currentReloadValue.Value += Time.deltaTime / reviveInterval; 
    }
}
