using System;
using UnityEngine;

public class GeneralGunManager : GunManager
{
    [Header("•œŠˆŠÔŠu(•b)")]
    [SerializeField] float reviveInterval;
    [SerializeField] JapaneseInputManager inputManager;

    public override void Shoot()
    {
        if (!bullet) { return; }
        if (currentReloadValue.Value < 1f) { return; }   //•â[‚³‚ê‚Ä‚È‚¢‚Æ‚«•Ô‚·

        BulletController b = bulletShooter.Value.Shoot();
        b.Yomigana = yomiganaSpawner.Value.Answer.Value;
        yomiganaSpawner.Value.OnShoot();
        Spawn();

        //ƒŠƒ[ƒh
        inputManager.ClearInput();
        currentReloadValue.Value = 0;
    }

    //ƒŠƒ[ƒh
    private void Update()
    {
        if(currentReloadValue.Value >= 1f) { return; }
        currentReloadValue.Value += Time.deltaTime / reviveInterval; 
    }
}
