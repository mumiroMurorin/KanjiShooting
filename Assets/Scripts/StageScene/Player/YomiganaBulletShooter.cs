using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YomiganaBulletShooter : MonoBehaviour, IBulletShooter
{
    Transform bulletShootedParent;

    public BulletController Bullet { private get; set; }

    private void Start()
    {
        Initialization();
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
        return Bullet;
    }
}
