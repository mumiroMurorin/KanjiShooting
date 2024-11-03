using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �_���[�W��^����
/// </summary>
public interface IDamager
{
    public void GiveDamage(IStatus status);
    public void GiveDamage(IKanjiStatus status);
}

/// <summary>
/// ���I�I�u�W�F�N�g��Transform�ɂ�铮��������
/// </summary>
public interface IMovable
{
    public void Move(Vector3 pos);
}

public interface IForceAddable
{
    public void AddForce(Vector3 vec);
}