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
    /// Shootというか爆発
    /// </summary>
    public override void Shoot()
    {
        isShooted = true;
        rb.isKinematic = false;

        // インスタンス化して爆破
        Instantiate(bombPrefab, this.gameObject.transform.position, Quaternion.identity).GetComponent<BombController>().Bomb();

        Destroy(this.gameObject);
    }
}
