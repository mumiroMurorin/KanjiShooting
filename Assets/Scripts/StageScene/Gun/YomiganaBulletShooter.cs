using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YomiganaBulletShooter : MonoBehaviour, IBulletShooter
{
    Transform bulletShootedParent;
    [SerializeField] AudioClip shootSE;

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

        // SEçƒê∂
        if (Bullet != null && shootSE != null) { Sound.SoundManager.Instance.PlaySE(shootSE); }
        return Bullet;
    }
}
