using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBulletSpawner : MonoBehaviour, IBulletSpawner
{
    [SerializeField] Transform parent;
    [SerializeField] GameObject bulletPrefab;

    /// <summary>
    /// �e�̐���
    /// </summary>
    /// <returns></returns>
    public BulletController Spawn()
    {
        if (!bulletPrefab) { Debug.LogError("�e���Z�b�g����ĂȂ���"); return null; }

        return Instantiate(bulletPrefab, parent).GetComponent<BulletController>();
    }
}
