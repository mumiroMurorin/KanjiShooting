using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVelocityLinearMotion : MonoBehaviour,IForceAddable
{
    [SerializeField] SerializeInterface<IStatus> status;
    [SerializeField] Rigidbody rigidBody;

    bool isActive;

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        rigidBody.isKinematic = !isActive;
    }

    public void AddForce(Vector3 vector)
    {
        if(!isActive) { return; }
        rigidBody.velocity = vector * status.Value.Speed.Value;
    }
}
