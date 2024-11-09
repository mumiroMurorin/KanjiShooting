using System;
using UnityEngine;

public class GeneralGunManager : GunManager
{
    [Header("復活間隔(秒)")]
    [SerializeField] float reviveInterval;
    [SerializeField] JapaneseInputManager inputManager;

    public override void Shoot()
    {
        if (!bullet) { return; }
        if (currentReloadValue.Value < 1f) { return; }   //補充されてないとき返す

        BulletController b = bulletShooter.Value.Shoot();
        b.Yomigana = yomiganaSpawner.Value.Answer.Value;
        yomiganaSpawner.Value.OnShoot();
        Spawn();

        //リロード
        inputManager.ClearInput();
        currentReloadValue.Value = 0;
    }

    //リロード
    private void Update()
    {
        if(currentReloadValue.Value >= 1f) { return; }
        currentReloadValue.Value += Time.deltaTime / reviveInterval; 
    }
}
