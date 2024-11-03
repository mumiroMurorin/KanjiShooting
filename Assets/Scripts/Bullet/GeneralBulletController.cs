using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBulletController : BulletController
{
    public override void Shoot()
    {
        isShooted = true;
        rb.AddForce(this.transform.forward * KanjiStatus.Value.Speed.Value, ForceMode.Acceleration);
    }
}
