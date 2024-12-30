using System;
using UnityEngine;

public class GeneralGunManager : GunManager
{
    [Header("•œŠˆŠÔŠu(•b)")]
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
        if (currentReloadValue.Value < 1f) { return; }   // •â[‚³‚ê‚Ä‚È‚¢‚Æ‚«•Ô‚·

        base.Shoot();

        // ƒŠƒ[ƒh
        inputHolder.Value.ClearInput();
        currentReloadValue.Value = 0;
    }

    // ƒŠƒ[ƒh
    private void Update()
    {
        if(currentReloadValue.Value >= 1f) { return; }
        currentReloadValue.Value += Time.deltaTime / reviveInterval; 
    }
}
