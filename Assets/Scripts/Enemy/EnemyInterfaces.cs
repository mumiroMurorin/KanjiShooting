using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public abstract void Initialize(EnemyInitializationData initialData);

    //public GameObject gameObject { get; }
}
