using System;
using UnityEngine;
using UniRx;
using VContainer;

public class ChargeGunManager : GunManager
{
    [SerializeField] JapaneseInputManager inputManager;

    [Header("復活間隔(秒)")]
    [SerializeField] float reviveInterval;
    [Header("撃つまでのチャージタイム")]
    [SerializeField] float specialBulletChargeTime;
    [Header("発射可能討伐数")]
    [SerializeField] int specialBulletCount = 5;

    [Header("通常弾スポナー")]
    [SerializeField] SerializeInterface<IBulletSpawner> generalBulletSpawner;
    [Header("スペシャル弾スポナー")]
    [SerializeField] SerializeInterface<IBulletSpawner> specialBulletSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> bulletShooter;
    [SerializeField] SerializeInterface<IYomiganaSpawner> yomiganaSpawner;

    // 特殊弾のチャージ進捗度
    ReactiveProperty<float> specialChargeValue = new ReactiveProperty<float>(0);
    public IReadOnlyReactiveProperty<float> SpecialChargeValue { get { return specialChargeValue; } }

    // 特殊弾を発射するまでのチャージタイム
    ReactiveProperty<float> specialShootValue = new ReactiveProperty<float>(0);
    public IReadOnlyReactiveProperty<float> SpecialShootValue { get { return specialShootValue; } }

    ScoreHolder scoreHolder;
    bool isCharging;
    bool isChargedSpecialBullet;

    [Inject]
    public void Construct(ScoreHolder holder)
    {
        scoreHolder = holder;
    }

    protected override void Initialize()
    {
        YomiganaSpawner = yomiganaSpawner.Value;
        BulletSpawner = generalBulletSpawner.Value;
        BulletShooter = bulletShooter.Value;

        Bind();
    }

    private void Bind()
    {
        scoreHolder.KillCountReactiveProperty
            .Pairwise()
            .Subscribe(pair => ChargeSpecialBullet(pair.Current - pair.Previous))
            .AddTo(this.gameObject);

        specialShootValue
            .Where(value => value >= 1)
            .Subscribe(value => yomiganaSpawner.Value.OnChargeComplete())
            .AddTo(this.gameObject);
    }

    public override void Shoot()
    {
        if (currentReloadValue.Value < 1f) { return; }   // 補充されてないとき返す

        // 撃つ弾をきめりゅ
        if (isChargedSpecialBullet) 
        { 
            BulletSpawner = specialBulletSpawner.Value;
            specialChargeValue.Value = 0f;
        }
        else 
        { 
            BulletSpawner = generalBulletSpawner.Value;
            yomiganaSpawner.Value.OnChargeCancell();
        }

        // しょっと
        base.Shoot();

        // リロード
        inputManager.ClearInput();
        currentReloadValue.Value = 0;
        ResetShootCharge();
    }

    public void StartCharge()
    {
        isCharging = true;
        if (specialChargeValue.Value >= 1) { yomiganaSpawner.Value.OnChargeStart(); }
    }

    /// <summary>
    /// スペシャル弾の発射チャージ
    /// </summary>
    private void ShootCharge()
    {
        // チャージ完了してるなら帰って
        if (specialShootValue.Value >= 1f) { return; }
        // スペシャルチャージ溜まってないなら帰ろう
        if (specialChargeValue.Value < 1) { return; }

        specialShootValue.Value += Time.deltaTime / specialBulletChargeTime;

        // チャージ完了してないなら帰って
        if (specialShootValue.Value < 1f) { return; }

        // チャージ完了したとき
        specialShootValue.Value = 1f;
        isChargedSpecialBullet = true;
        isCharging = false;
    }

    /// <summary>
    /// スペシャル弾チャージタイムのリセット
    /// </summary>
    private void ResetShootCharge()
    {
        specialShootValue.Value = 0;
        isChargedSpecialBullet = false;
        isCharging = false;
    }

    /// <summary>
    /// スペシャル弾チャージ
    /// </summary>
    private void ChargeSpecialBullet(int killNum)
    {
        specialChargeValue.Value = Mathf.Clamp(specialChargeValue.Value + (float)killNum / specialBulletCount, 0, 1f);
    }

    private void Update()
    {
        // リロード
        if (currentReloadValue.Value < 1f) 
        { 
            currentReloadValue.Value += Time.deltaTime / reviveInterval;
        }

        // スペシャルチャージ
        if(isCharging)
        {
            ShootCharge();
        }
        
    }
}
