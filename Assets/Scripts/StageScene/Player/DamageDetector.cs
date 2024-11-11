using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    [SerializeField] bool isKanjiStatus;
    [SerializeField] SerializeInterface<IStatus> status;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out IDamager damager)) { return; }

        //�_���[�W��^���鏈��
        if (isKanjiStatus) { damager.GiveDamage(status.Value as IKanjiStatus); }
        else { damager.GiveDamage(status.Value); }
    }
}
