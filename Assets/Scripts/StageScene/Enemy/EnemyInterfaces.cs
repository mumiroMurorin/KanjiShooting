using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IEnemy
{
    public abstract void Initialize(EnemyInitializationData initialData);

    public abstract void Despawn();
}

public interface IAttachableItemOnDestroy
{
    public Action<Transform> OnDestroyEvent { get; set; }
}
