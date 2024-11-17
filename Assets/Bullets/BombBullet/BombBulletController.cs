using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BombBulletController : BulletController
{
    [SerializeField] GameObject bombPrefab;

    protected override void Initialize()
    {
        
    }

    public override void Shoot()
    {
        isShooted = true;
        rb.isKinematic = false;
        rb.AddForce(this.transform.forward * KanjiStatus.Value.Speed.Value, ForceMode.Acceleration);
    }

    protected override void AfterKillEnemy()
    {
        // インスタンス化して爆破
        Instantiate(bombPrefab, this.gameObject.transform.position, Quaternion.identity).GetComponent<BombController>().Bomb();

        Destroy(this.gameObject);
    }
}
