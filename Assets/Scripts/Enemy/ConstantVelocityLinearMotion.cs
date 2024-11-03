using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVelocityLinearMotion : MonoBehaviour,IForceAddable
{
    [SerializeField] SerializeInterface<IStatus> status;
    [SerializeField] Rigidbody rigidBody;

    public void AddForce(Vector3 vector)
    {
        rigidBody.velocity = vector * status.Value.Speed.Value;
    }
}
