using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BombStatus : MonoBehaviour, IStatus
{
    [SerializeField] int attackInit = 1;

    public MobLayer Layer => MobLayer.Bullet;

    ReactiveProperty<int> hp = new ReactiveProperty<int>(int.MaxValue);
    public IReadOnlyReactiveProperty<int> HP { get { return hp; } }

    ReactiveProperty<float> hpNormalized = new ReactiveProperty<float>(float.MaxValue);
    public IReadOnlyReactiveProperty<float> HPNormalized { get { return hpNormalized; } }

    ReactiveProperty<int> attack = new ReactiveProperty<int>(0);
    public IReadOnlyReactiveProperty<int> Attack { get { return attack; } }

    ReactiveProperty<int> speed = new ReactiveProperty<int>(0);
    public IReadOnlyReactiveProperty<int> Speed { get { return speed; } }

    private void Start()
    {
        attack = new ReactiveProperty<int>(attackInit);
    }

    public void SetAttack(int value)
    {
        attack.Value = value;
    }

    public void SetHP(int value)
    {
        hp.Value = value;   
    }

    public void SetSpeed(int value)
    {
        speed.Value = value;
    }
}
