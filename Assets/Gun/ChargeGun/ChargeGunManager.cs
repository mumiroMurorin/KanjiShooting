using System;
using UnityEngine;
using UniRx;
using VContainer;

public class ChargeGunManager : GunManager
{
    [SerializeField] JapaneseInputManager inputManager;

    [Header("�����Ԋu(�b)")]
    [SerializeField] float reviveInterval;

    [Header("�ʏ�e")]
    [SerializeField] SerializeInterface<IBulletSpawner> generalSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> generalShooter;
    [Header("����e")]
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
        if (currentReloadValue.Value < 1f) { return; }   // ��[����ĂȂ��Ƃ��Ԃ�

        // ����e�̃`���[�W�����܂��Ă��������e���U
        if (specialShootCharger.Value.ChargeCount.Value >= 1) 
        { 
            BulletSpawner = specialSpawner.Value;
            BulletShooter = specialShooter.Value;
        }
        // �e���ĂȂ��������ʒe�𑕓U
        else 
        { 
            BulletSpawner = generalSpawner.Value;
            BulletShooter = generalShooter.Value;
            // Shoot�`���[�W���L�����Z��
            yomiganaSpawner.Value.OnChargeCancell();
        }

        // �������
        base.Shoot();

        // �����[�h
        inputManager.ClearInput();
        currentReloadValue.Value = 0;
        specialShootCharger.Value.ResetCharge();
    }

    private void Update()
    {
        // �����[�h
        if (currentReloadValue.Value < 1f) 
        { 
            currentReloadValue.Value += Time.deltaTime / reviveInterval;
        }        
    }
}
