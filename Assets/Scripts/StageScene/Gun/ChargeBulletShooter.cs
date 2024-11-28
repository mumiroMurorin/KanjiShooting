using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ChargeBulletShooter : MonoBehaviour, IBulletShootCharger
{
    [Header("撃つまでのチャージタイム")]
    [SerializeField] float bulletChargeTime;
    [SerializeField] AudioClip shootSE;

    [SerializeField] SerializeInterface<IBulletReloadCharger> reloadCharger;
    [SerializeField] SerializeInterface<IYomiganaSpawner> yomiganaSpawner;

    public BulletController Bullet { private get; set; }

    // 特殊弾を発射するまでのチャージタイム経過率
    ReactiveProperty<float> chargeCount = new ReactiveProperty<float>(0);
    public IReadOnlyReactiveProperty<float> ChargeCount { get { return chargeCount; } }

    Transform bulletShootedParent;
    bool isCharging;

    private void Start()
    {
        Initialization();
        Bind();
    }

    private void Bind()
    {
        // 特殊弾の発射準備完了 → 読み仮名オブジェクト発火
        ChargeCount
            .Where(value => value >= 1)
            .Subscribe(value => yomiganaSpawner.Value.OnChargeComplete())
            .AddTo(this.gameObject);
    }

    private void Initialization()
    {
        if (bulletShootedParent) { Destroy(bulletShootedParent); }
        bulletShootedParent = new GameObject("BulletShootedParent").transform;
    }

    public BulletController Shoot()
    {
        Bullet?.transform.SetParent(bulletShootedParent);
        Bullet?.Shoot();

        // ショットチャージのリセット
        ResetCharge();
        // リロードチャージのリセット
        reloadCharger.Value.ResetCharge();

        // SE再生
        if (Bullet != null && shootSE != null) { Sound.SoundManager.Instance.PlaySE(shootSE); }
        return Bullet;
    }

    /// <summary>
    /// チャージスタート
    /// </summary>
    public void StartCharge()
    {
        isCharging = true;

        // リロードチャージが必要な場合
        if (reloadCharger != null && reloadCharger.Value.ChargeCount.Value >= 1) 
        { yomiganaSpawner.Value.OnChargeStart(); }
    }

    public void Charge(float amount)
    {
        // チャージ完了してるなら帰って
        if (ChargeCount.Value >= 1f) { return; }
        // リロードチャージが完了してないとき帰って
        if (reloadCharger == null || (reloadCharger != null && reloadCharger.Value.ChargeCount.Value < 1f)) { return; }

        // チャージ
        chargeCount.Value += amount / bulletChargeTime;

        // チャージ完了したとき
        if (ChargeCount.Value >= 1f) 
        {
            chargeCount.Value = 1f;
            isCharging = false;
        }
    }

    /// <summary>
    /// チャージタイムリセット
    /// </summary>
    public void ResetCharge()
    {
        chargeCount.Value = 0;
        isCharging = false;
    }

    private void Update()
    {
        // チャージ
        if (isCharging) { Charge(Time.deltaTime); }
    }
}
