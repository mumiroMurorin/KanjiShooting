using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

public class PlayerStatus : MonoBehaviour, IStatus
{
    [Header("MaxHP")]
    [SerializeField] int maxHP;
    [Header("初期HP")]
    [SerializeField] int HPInit;
    [Header("初期攻撃力")]
    [SerializeField] int attackInit;
    [Header("初期スピード")]
    [SerializeField] int speedInit;

    [Header("Delegate on Change HP")]
    [SerializeField] UnityEvent<int> OnChangeHP;
    [Header("Delegate on Change HPNormalized")]
    [SerializeField] UnityEvent<float> OnChangeHPNormalized;
    [Header("Delegate on Change Attack")]
    [SerializeField] UnityEvent<int> OnChangeAttack;
    [Header("Delegate on Change Speed")]
    [SerializeField] UnityEvent<int> OnChangeSpeed;

    ReactiveProperty<int> hp;
    ReactiveProperty<int> attack;
    ReactiveProperty<int> speed;

    public MobLayer Layer { get; } = MobLayer.Player;

    public IReadOnlyReactiveProperty<int> HP
    { 
        get { return hp; }
    }

    public IReadOnlyReactiveProperty<float> HPNormalized { get; private set; }

    public IReadOnlyReactiveProperty<int> Attack 
    { 
        get { return attack; }
    }

    public IReadOnlyReactiveProperty<int> Speed 
    {
        get { return speed; }
    }

    private void Awake()
    {
        Initialize();
        Bind();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Initialize()
    {
        
    }

    private void Bind()
    {
        hp = new ReactiveProperty<int>(HPInit);
        hp.Subscribe(h => OnChangeHP.Invoke(h))
          .AddTo(this);

        HPNormalized = hp.Select(currentHp => (float)currentHp / maxHP)
                         .ToReactiveProperty();
        HPNormalized.Subscribe(h => OnChangeHPNormalized.Invoke(h))
                    .AddTo(this);

        attack = new ReactiveProperty<int>(attackInit);
        attack.Subscribe(a => OnChangeAttack.Invoke(a))
              .AddTo(this);

        speed = new ReactiveProperty<int>(speedInit);
        speed.Subscribe(s => OnChangeSpeed.Invoke(s))
              .AddTo(this);
    }

    public void SetHP(int value)
    {
        if(hp == null) { Debug.LogError("ReactiveProperty:hpが初期化されていません"); return; }
        hp.Value = Mathf.Clamp(value, 0, maxHP);
    }

    public void SetAttack(int value) 
    {
        if (attack == null) { Debug.LogError("ReactiveProperty:attackが初期化されていません"); return; }
        attack.Value = value;
    }

    public void SetSpeed(int value)
    {
        if (speed == null) { Debug.LogError("ReactiveProperty:speedが初期化されていません"); return; }
        speed.Value = value;
    }
}
