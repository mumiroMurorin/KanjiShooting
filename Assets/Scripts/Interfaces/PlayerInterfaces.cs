using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤー、敵の向き(視点)移動を実装する
/// </summary>
public interface IRotatable
{
    public void Rotate(Vector3 rotation);
}

/// <summary>
/// ステータス情報の保持、運用
/// </summary>
public interface IStatus
{
    public MobLayer Layer { get; }

    public IReadOnlyReactiveProperty<int> HP { get; }

    public IReadOnlyReactiveProperty<float> HPNormalized { get; }

    public IReadOnlyReactiveProperty<int> Attack { get; }

    public IReadOnlyReactiveProperty<int> Speed { get; }

    public void SetHP(int value);
    public void SetAttack(int value);
    public void SetSpeed(int value);
}

public enum MobLayer
{
    Enemy,
    Player,
    Bullet
}