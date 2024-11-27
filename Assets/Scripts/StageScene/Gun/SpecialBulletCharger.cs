using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;

public class SpecialBulletCharger : MonoBehaviour , IBulletReloadCharger
{
    [Header("発射可能討伐数")]
    [SerializeField] int chargeAmount = 5;

    // 特殊弾のチャージ進捗度
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
    /// チャージ
    /// </summary>
    /// <param name="amount"></param>
    public void Charge(float amount)
    {
        chargeCount.Value = Mathf.Clamp(chargeCount.Value + amount, 0f, 1f);
    }

    /// <summary>
    /// チャージ時間のリセット
    /// </summary>
    void IBulletReloadCharger.ResetCharge()
    {
        chargeCount.Value = 0;
    }
}
