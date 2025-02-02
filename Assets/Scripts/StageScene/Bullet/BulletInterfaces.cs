using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 読み仮名オブジェクトの生成
/// </summary>
public interface IYomiganaSpawner
{
    public IReadOnlyReactiveProperty<string> Answer { get; }

    public Transform BulletTransform { set; }

    /// <summary>
    /// 読み仮名への文字追加
    /// </summary>
    /// <param name="c"></param>
    public void OnChangeAnswer(string str);

    public void OnShoot();
}

public interface IChargeYomiganaSpawner : IYomiganaSpawner
{
    public void OnChargeStart();

    public void OnChargeCancell();

    public void OnChargeComplete();
}

/// <summary>
/// 弾の発射
/// </summary>
public interface IBulletShooter
{
    public BulletController Bullet { set; }

    public BulletController Shoot();
}

public interface IBulletReloadCharger
{
    IReadOnlyReactiveProperty<float> ChargeCount { get; }

    void Charge(float amount);

    void ResetCharge();
}

public interface IBulletShootCharger : IBulletShooter
{
    IReadOnlyReactiveProperty<float> ChargeCount { get; }

    void StartCharge();

    void ResetCharge();

}

public interface IKanjiStatus : IStatus
{
    public IReadOnlyReactiveProperty<Kanji.QuestionData> Question { get; }

    public void SetQuestion(Kanji.QuestionData questionData);
}

public interface IBulletStatus : IStatus
{
    public IReadOnlyReactiveProperty<string> Answer { get; }

    public void SetAnswer(string answer);
}

/// <summary>
/// 弾の生成
/// </summary>
public interface IBulletSpawner
{
    public BulletController Spawn();
}