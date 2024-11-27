using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;

public class SpecialBulletCharger : MonoBehaviour , IBulletReloadCharger
{
    [Header("���ˉ\������")]
    [SerializeField] int chargeAmount = 5;

    // ����e�̃`���[�W�i���x
    ReactiveProperty<float> chargeCount = new ReactiveProperty<float>(0);
    IReadOnlyReactiveProperty<float> IBulletReloadCharger.ChargeCount { get { return chargeCount; } }

    ScoreHolder scoreHolder;

    [Inject]
    public void Construct(ScoreHolder holder)
    {
        scoreHolder = holder;
    }

    private void Start()
    {
        Bind();
    }

    private void Bind()
    {
        scoreHolder.KillCountReactiveProperty
            .Pairwise()
            .Subscribe(pair => Charge((pair.Current - pair.Previous) / (float)chargeAmount))
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// �`���[�W
    /// </summary>
    /// <param name="amount"></param>
    public void Charge(float amount)
    {
        chargeCount.Value = Mathf.Clamp(chargeCount.Value + amount, 0f, 1f);
    }

    /// <summary>
    /// �`���[�W���Ԃ̃��Z�b�g
    /// </summary>
    void IBulletReloadCharger.ResetCharge()
    {
        chargeCount.Value = 0;
    }
}
