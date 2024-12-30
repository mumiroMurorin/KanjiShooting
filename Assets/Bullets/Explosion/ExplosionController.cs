using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionController : BulletController
{
    [SerializeField] GameObject bombPrefab;

    protected override void Initialize()
    {
        
    }

    /// <summary>
    /// Shoot�Ƃ���������
    /// </summary>
    public override void Shoot()
    {
        isShooted = true;
        rb.isKinematic = false;

        // �C���X�^���X�����Ĕ��j
        Instantiate(bombPrefab, this.gameObject.transform.position, Quaternion.identity).GetComponent<BombController>().Bomb();

        Destroy(this.gameObject);
    }
}
