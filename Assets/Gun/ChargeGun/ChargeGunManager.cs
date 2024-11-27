using System;
using UnityEngine;
using UniRx;
using VContainer;

public class ChargeGunManager : GunManager
{
    [SerializeField] JapaneseInputManager inputManager;

    [Header("•œŠˆŠÔŠu(•b)")]
    [SerializeField] float reviveInterval;

    [Header("’Êí’e")]
    [SerializeField] SerializeInterface<IBulletSpawner> generalSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> generalShooter;
    [Header("“Áê’e")]
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
        if (currentReloadValue.Value < 1f) { return; }   // •â[‚³‚ê‚Ä‚È‚¢‚Æ‚«•Ô‚·

        // “Áê’e‚Ìƒ`ƒƒ[ƒW‚ª—­‚Ü‚Á‚Ä‚¢‚½‚ç“Áê’e‘•“U
        if (specialShootCharger.Value.ChargeCount.Value >= 1) 
        { 
            BulletSpawner = specialSpawner.Value;
            BulletShooter = specialShooter.Value;
        }
        // ’e‚Á‚Ä‚È‚©‚Á‚½‚çˆê”Ê’e‚ğ‘•“U
        else 
        { 
            BulletSpawner = generalSpawner.Value;
            BulletShooter = generalShooter.Value;
            // Shootƒ`ƒƒ[ƒW‚ğƒLƒƒƒ“ƒZƒ‹
            yomiganaSpawner.Value.OnChargeCancell();
        }

        // ‚µ‚å‚Á‚Æ
        base.Shoot();

        // ƒŠƒ[ƒh
        inputManager.ClearInput();
        currentReloadValue.Value = 0;
        specialShootCharger.Value.ResetCharge();
    }

    private void Update()
    {
        // ƒŠƒ[ƒh
        if (currentReloadValue.Value < 1f) 
        { 
            currentReloadValue.Value += Time.deltaTime / reviveInterval;
        }        
    }
}
