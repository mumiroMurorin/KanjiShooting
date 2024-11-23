using System;
using UnityEngine;
using UniRx;
using VContainer;

public class ChargeGunManager : GunManager
{
    [SerializeField] JapaneseInputManager inputManager;

    [Header("�����Ԋu(�b)")]
    [SerializeField] float reviveInterval;
    [Header("���܂ł̃`���[�W�^�C��")]
    [SerializeField] float specialBulletChargeTime;
    [Header("���ˉ\������")]
    [SerializeField] int specialBulletCount = 5;

    [Header("�ʏ�e�X�|�i�[")]
    [SerializeField] SerializeInterface<IBulletSpawner> generalBulletSpawner;
    [Header("�X�y�V�����e�X�|�i�[")]
    [SerializeField] SerializeInterface<IBulletSpawner> specialBulletSpawner;
    [SerializeField] SerializeInterface<IBulletShooter> bulletShooter;
    [SerializeField] SerializeInterface<IYomiganaSpawner> yomiganaSpawner;

    // ����e�̃`���[�W�i���x
    ReactiveProperty<float> specialChargeValue = new ReactiveProperty<float>(0);
    public IReadOnlyReactiveProperty<float> SpecialChargeValue { get { return specialChargeValue; } }

    // ����e�𔭎˂���܂ł̃`���[�W�^�C��
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
        if (currentReloadValue.Value < 1f) { return; }   // ��[����ĂȂ��Ƃ��Ԃ�

        // ���e�����߂��
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

        // �������
        base.Shoot();

        // �����[�h
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
    /// �X�y�V�����e�̔��˃`���[�W
    /// </summary>
    private void ShootCharge()
    {
        // �`���[�W�������Ă�Ȃ�A����
        if (specialShootValue.Value >= 1f) { return; }
        // �X�y�V�����`���[�W���܂��ĂȂ��Ȃ�A�낤
        if (specialChargeValue.Value < 1) { return; }

        specialShootValue.Value += Time.deltaTime / specialBulletChargeTime;

        // �`���[�W�������ĂȂ��Ȃ�A����
        if (specialShootValue.Value < 1f) { return; }

        // �`���[�W���������Ƃ�
        specialShootValue.Value = 1f;
        isChargedSpecialBullet = true;
        isCharging = false;
    }

    /// <summary>
    /// �X�y�V�����e�`���[�W�^�C���̃��Z�b�g
    /// </summary>
    private void ResetShootCharge()
    {
        specialShootValue.Value = 0;
        isChargedSpecialBullet = false;
        isCharging = false;
    }

    /// <summary>
    /// �X�y�V�����e�`���[�W
    /// </summary>
    private void ChargeSpecialBullet(int killNum)
    {
        specialChargeValue.Value = Mathf.Clamp(specialChargeValue.Value + (float)killNum / specialBulletCount, 0, 1f);
    }

    private void Update()
    {
        // �����[�h
        if (currentReloadValue.Value < 1f) 
        { 
            currentReloadValue.Value += Time.deltaTime / reviveInterval;
        }

        // �X�y�V�����`���[�W
        if(isCharging)
        {
            ShootCharge();
        }
        
    }
}
