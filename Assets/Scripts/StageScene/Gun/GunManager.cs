using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class GunManager : MonoBehaviour
{    
    IYomiganaSpawner yomiganaSpawner;
    IBulletShooter bulletShooter;
    IBulletSpawner bulletSpawner;

    protected IYomiganaSpawner YomiganaSpawner { set { yomiganaSpawner = value; } }
    protected IBulletShooter BulletShooter { set { bulletShooter = value; } }
    protected IBulletSpawner BulletSpawner { set { bulletSpawner = value; } }

    protected BulletController bullet;
    protected ReactiveProperty<float> currentReloadValue = new ReactiveProperty<float>(1f);

    GameObject hiraganaParent;

    public IReadOnlyReactiveProperty<float> ReloadValue
    {
        get { return currentReloadValue; }
    }

    private void Start()
    {
        Initialize();
        SpawnHiraganaParent();
    }

    protected abstract void Initialize();

    /// <summary>
    /// �ꎞ�I�ɂЂ炪�ȃI�u�W�F�N�g���������e�����
    /// </summary>
    private void SpawnHiraganaParent()
    {
        hiraganaParent = new GameObject("HiraganaParent");
        SetParent(hiraganaParent.transform, this.transform);

        yomiganaSpawner.BulletTransform = hiraganaParent.transform;
    }

    /// <summary>
    /// �e�̐���
    /// </summary>
    private void SpawnBullet()
    {
        bullet = bulletSpawner.Spawn();
        bulletShooter.Bullet = bullet;
    }

    /// <summary>
    /// Transform.SetParent()�Ɉʒu�Ɗp�x�̓���������ǉ�
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    private void SetParent(Transform child, Transform parent)
    {
        child.transform.SetParent(parent);
        child.transform.position = parent.position;
        child.transform.eulerAngles = parent.eulerAngles;
    }

    /// <summary>
    /// �e�̔���
    /// </summary>
    /// <returns></returns>
    public virtual void Shoot()
    {
        SpawnBullet();

        // �\�ߗp�ӂ��Ă������������I�u�W�F�N�g�𐶐������e�ɏ������
        SetParent(hiraganaParent.transform, bullet.transform);

        BulletController b = bulletShooter.Shoot();
        b.Yomigana = yomiganaSpawner.Answer.Value;
        yomiganaSpawner.OnShoot();

        // �ēx�Ђ炪�ȃI�u�W�F�N�g�̐e�𐶐�
        SpawnHiraganaParent();
    }
}
