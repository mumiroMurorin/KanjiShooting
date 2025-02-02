using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    [SerializeField] bool isKanjiStatus;
    [SerializeField] SerializeInterface<IStatus> status;

    GameObject enteredObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out IDamager damager)) { return; }
        if(collision.gameObject == enteredObject) { return; }

        enteredObject = collision.gameObject;

        // Debug.Log($"きちゃ: {collision.gameObject.name}");

        //ダメージを与える処理
        if (isKanjiStatus) { damager.GiveDamage(status.Value as IKanjiStatus); }
        else { damager.GiveDamage(status.Value); }
    }
}
