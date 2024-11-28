using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ChargeBulletShooter : MonoBehaviour, IBulletShootCharger
{
    [Header("���܂ł̃`���[�W�^�C��")]
    [SerializeField] float bulletChargeTime;
    [SerializeField] AudioClip shootSE;

    [SerializeField] SerializeInterface<IBulletReloadCharger> reloadCharger;
    [SerializeField] SerializeInterface<IYomiganaSpawner> yomiganaSpawner;

    public BulletController Bullet { private get; set; }

    // ����e�𔭎˂���܂ł̃`���[�W�^�C���o�ߗ�
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
        // ����e�̔��ˏ������� �� �ǂ݉����I�u�W�F�N�g����
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

        // �V���b�g�`���[�W�̃��Z�b�g
        ResetCharge();
        // �����[�h�`���[�W�̃��Z�b�g
        reloadCharger.Value.ResetCharge();

        // SE�Đ�
        if (Bullet != null && shootSE != null) { Sound.SoundManager.Instance.PlaySE(shootSE); }
        return Bullet;
    }

    /// <summary>
    /// �`���[�W�X�^�[�g
    /// </summary>
    public void StartCharge()
    {
        isCharging = true;

        // �����[�h�`���[�W���K�v�ȏꍇ
        if (reloadCharger != null && reloadCharger.Value.ChargeCount.Value >= 1) 
        { yomiganaSpawner.Value.OnChargeStart(); }
    }

    public void Charge(float amount)
    {
        // �`���[�W�������Ă�Ȃ�A����
        if (ChargeCount.Value >= 1f) { return; }
        // �����[�h�`���[�W���������ĂȂ��Ƃ��A����
        if (reloadCharger == null || (reloadCharger != null && reloadCharger.Value.ChargeCount.Value < 1f)) { return; }

        // �`���[�W
        chargeCount.Value += amount / bulletChargeTime;

        // �`���[�W���������Ƃ�
        if (ChargeCount.Value >= 1f) 
        {
            chargeCount.Value = 1f;
            isCharging = false;
        }
    }

    /// <summary>
    /// �`���[�W�^�C�����Z�b�g
    /// </summary>
    public void ResetCharge()
    {
        chargeCount.Value = 0;
        isCharging = false;
    }

    private void Update()
    {
        // �`���[�W
        if (isCharging) { Charge(Time.deltaTime); }
    }
}
