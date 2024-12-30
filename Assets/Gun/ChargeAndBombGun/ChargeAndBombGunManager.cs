using System;
using UnityEngine;
using UniRx;
using VContainer;

public class ChargeAndBombGunManager : GunManager
{
    [SerializeField] SerializeInterface<IJapaneseInputHolder> inputHolder;

    [Header("復活間隔(秒)")]
    [SerializeField] float reviveInterval;

    [Header("通常弾")]
    [SerializeField] SerializeInterface<IBulletSpawner> generalSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> generalShooter;
    [Header("特殊弾")]
    [SerializeField] SerializeInterface<IBulletSpawner> specialSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> specialShooter;
    [SerializeField] SerializeInterface<IBulletShootCharger> specialShootCharger;
    [Header("ボム")]
    [SerializeField] SerializeInterface<IBulletSpawner> bombSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> bombShooter;
    [SerializeField] SerializeInterface<IBulletShootCharger> bombShootCharger;

    [Space(20)]
    [SerializeField] SerializeInterface<IChargeYomiganaSpawner> yomiganaSpawner;

    protected override void Initialize()
    {
        YomiganaSpawner = yomiganaSpawner.Value;
        BulletSpawner = generalSpawner.Value;
        BulletShooter = generalShooter.Value;
    }

    /// <summary>
    /// スペシャル弾チャージする
    /// </summary>
    public void StartCharge()
    {
        // 答えが入力されていないとき自爆チャージする
        if (inputHolder.Value.GetAnswer().Length <= 0) 
        {
            bombShootCharger.Value.StartCharge();
        }
        // 答えが入力されているときはスペシャル弾のチャージ
        else
        {
            specialShootCharger.Value.StartCharge();
        }
    }

    public override void Shoot()
    {
        // 文字が入力されていない且つボムチャージされてないとき返す
        if (inputHolder.Value.GetAnswer().Length <= 0 && 
            bombShootCharger.Value.ChargeCount.Value < 1f) 
        { return; }

        // 補充されてないとき返す
        if (currentReloadValue.Value < 1f) { return; }   

        // 特殊弾のチャージが溜まっていたら特殊弾装填
        if (specialShootCharger.Value.ChargeCount.Value >= 1) 
        { 
            BulletSpawner = specialSpawner.Value;
            BulletShooter = specialShooter.Value;
        }
        // ボムのチャージが溜まっていたらボム装填
        else if (bombShootCharger.Value.ChargeCount.Value >= 1)
        {
            BulletSpawner = bombSpawner.Value;
            BulletShooter = bombShooter.Value;
        }
        // たまってなかったら一般弾を装填
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
        inputHolder.Value.ClearInput();
        currentReloadValue.Value = 0;
        specialShootCharger.Value.ResetCharge();
        bombShootCharger.Value.ResetCharge();
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
