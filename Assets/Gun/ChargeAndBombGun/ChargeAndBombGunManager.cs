using System;
using UnityEngine;
using UniRx;
using VContainer;

public class ChargeAndBombGunManager : GunManager
{
    [SerializeField] SerializeInterface<IJapaneseInputHolder> inputHolder;

    [Header("�����Ԋu(�b)")]
    [SerializeField] float reviveInterval;

    [Header("�ʏ�e")]
    [SerializeField] SerializeInterface<IBulletSpawner> generalSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> generalShooter;
    [Header("����e")]
    [SerializeField] SerializeInterface<IBulletSpawner> specialSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> specialShooter;
    [SerializeField] SerializeInterface<IBulletShootCharger> specialShootCharger;
    [Header("�{��")]
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
    /// �X�y�V�����e�`���[�W����
    /// </summary>
    public void StartCharge()
    {
        // ���������͂���Ă��Ȃ��Ƃ������`���[�W����
        if (inputHolder.Value.GetAnswer().Length <= 0) 
        {
            bombShootCharger.Value.StartCharge();
        }
        // ���������͂���Ă���Ƃ��̓X�y�V�����e�̃`���[�W
        else
        {
            specialShootCharger.Value.StartCharge();
        }
    }

    public override void Shoot()
    {
        // ���������͂���Ă��Ȃ����{���`���[�W����ĂȂ��Ƃ��Ԃ�
        if (inputHolder.Value.GetAnswer().Length <= 0 && 
            bombShootCharger.Value.ChargeCount.Value < 1f) 
        { return; }

        // ��[����ĂȂ��Ƃ��Ԃ�
        if (currentReloadValue.Value < 1f) { return; }   

        // ����e�̃`���[�W�����܂��Ă��������e���U
        if (specialShootCharger.Value.ChargeCount.Value >= 1) 
        { 
            BulletSpawner = specialSpawner.Value;
            BulletShooter = specialShooter.Value;
        }
        // �{���̃`���[�W�����܂��Ă�����{�����U
        else if (bombShootCharger.Value.ChargeCount.Value >= 1)
        {
            BulletSpawner = bombSpawner.Value;
            BulletShooter = bombShooter.Value;
        }
        // ���܂��ĂȂ��������ʒe�𑕓U
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
        inputHolder.Value.ClearInput();
        currentReloadValue.Value = 0;
        specialShootCharger.Value.ResetCharge();
        bombShootCharger.Value.ResetCharge();
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
