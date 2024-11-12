using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// �ǂ݉����I�u�W�F�N�g�̐���
/// </summary>
public interface IYomiganaSpawner
{
    public IReadOnlyReactiveProperty<string> Answer { get; }

    public Transform BulletTransform { set; }

    /// <summary>
    /// �ǂ݉����ւ̕����ǉ�
    /// </summary>
    /// <param name="c"></param>
    public void OnChangeAnswer(string str);

    public void OnShoot();
}

/// <summary>
/// �e�̔���
/// </summary>
public interface IBulletShooter
{
    public BulletController Bullet { set; }

    public BulletController Shoot();
}

public interface IKanjiStatus : IStatus
{
    public IReadOnlyReactiveProperty<string[]> Answers { get; }

    public void SetAnswers(string[] value);
}

/// <summary>
/// �e�̐���
/// </summary>
public interface IBulletSpawner
{
    public BulletController Spawn();
}