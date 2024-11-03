using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージを与える
/// </summary>
public interface IDamager
{
    public void GiveDamage(IStatus status);
    public void GiveDamage(IKanjiStatus status);
}

/// <summary>
/// 動的オブジェクトのTransformによる動きを実装
/// </summary>
public interface IMovable
{
    public void Move(Vector3 pos);
}

public interface IForceAddable
{
    public void AddForce(Vector3 vec);
}