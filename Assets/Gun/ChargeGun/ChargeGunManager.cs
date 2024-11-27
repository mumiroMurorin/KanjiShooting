using System;
using UnityEngine;
using UniRx;
using VContainer;

public class ChargeGunManager : GunManager
{
    [SerializeField] JapaneseInputManager inputManager;

    [Header("復活間隔(秒)")]
    [SerializeField] float reviveInterval;

    [Header("通常弾")]
    [SerializeField] SerializeInterface<IBulletSpawner> generalSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> generalShooter;
    [Header("特殊弾")]
    [SerializeField] SerializeInterface<IBulletSpawner> specialSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> specialShooter;
    [SerializeField] SerializeInterface<IBulletShootCharger> specialShootCharger;

    [Space(20)]
    [SerializeField] SerializeInterface<IYomiganaSpawner> yomiganaSpawner;

    protected override void Initialize()
    {
        YomiganaSpawner = yomiganaSpawner.Value;
        BulletSpawner = generalSpawner.Value;
        BulletShooter = generalShooter.Value;
    }

    public override void Shoot()
    {
        if (currentReloadValue.Value < 1f) { return; }   // 補充されてないとき返す

        // 特殊弾のチャージが溜まっていたら特殊弾装填
        if (specialShootCharger.Value.ChargeCount.Value >= 1) 
        { 
            BulletSpawner = specialSpawner.Value;
            BulletShooter = specialShooter.Value;
        }
        // 弾ってなかったら一般弾を装填
        else 
        { 
            BulletSpawner = generalSpawner.Value;
            BulletShooter = generalShooter.Value;
            // Shootチャージをキャンセル
            yomiganaSpawner.Value.OnChargeCancell();
        }

        // しょっと
        base.Shoot();

        // リロード
        inputManager.ClearInput();
        currentReloadValue.Value = 0;
        specialShootCharger.Value.ResetCharge();
    }

    private void Update()
    {
        // リロード
        if (currentReloadValue.Value < 1f) 
        { 
            currentReloadValue.Value += Time.deltaTime / reviveInterval;
        }        
    }
}
