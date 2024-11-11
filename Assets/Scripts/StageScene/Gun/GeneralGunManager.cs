using System;
using UnityEngine;

public class GeneralGunManager : GunManager
{
    [Header("�����Ԋu(�b)")]
    [SerializeField] float reviveInterval;
    [SerializeField] JapaneseInputManager inputManager;

    public override void Shoot()
    {
        if (!bullet) { return; }
        if (currentReloadValue.Value < 1f) { return; }   //��[����ĂȂ��Ƃ��Ԃ�

        BulletController b = bulletShooter.Value.Shoot();
        b.Yomigana = yomiganaSpawner.Value.Answer.Value;
        yomiganaSpawner.Value.OnShoot();
        Spawn();

        //�����[�h
        inputManager.ClearInput();
        currentReloadValue.Value = 0;
    }

    //�����[�h
    private void Update()
    {
        if(currentReloadValue.Value >= 1f) { return; }
        currentReloadValue.Value += Time.deltaTime / reviveInterval; 
    }
}
