using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBulletController : BulletController
{
    protected override void Initialize()
    {
    }

    public override void Shoot()
    {
        isShooted = true;
        rb.isKinematic = false;
        rb.AddForce(this.transform.forward * KanjiStatus.Value.Speed.Value, ForceMode.Acceleration);
    }
}
