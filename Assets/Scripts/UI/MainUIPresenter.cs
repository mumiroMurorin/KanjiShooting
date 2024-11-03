using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MainUIPresenter : MonoBehaviour
{
    [SerializeField] PlayerHPBarView playerHPBarView;
    [SerializeField] PlayerHPTextView playerHPTextView;
    [SerializeField] BulletReloadGageView bulletReloadGageView;
    [SerializeField] KillCountTextView killCountTextView;
    [SerializeField] DamageEffectView damageEffectView;

    [SerializeField] SerializeInterface<IStatus> playerStatus_model;
    [SerializeField] ScoreManager scoreManager_model;
    [SerializeField] GunManager gunManager_model;

    private void Start()
    {
        SetEvents();
        Bind();
    }

    private void SetEvents()
    {

    }

    private void Bind()
    {
        playerStatus_model.Value.HPNormalized
            .Subscribe(playerHPBarView.OnChangeHP)
            .AddTo(this.gameObject);

        playerStatus_model.Value.HPNormalized
            .Pairwise()
            .Where(pair => pair.Current < pair.Previous) // Œ¸­‚µ‚Ä‚¢‚é‚Æ‚«‚Ì‚Ý
            .Subscribe(pair => {
                damageEffectView.OnDamage(pair.Previous - pair.Current);
            });

        playerStatus_model.Value.HP
            .Subscribe(playerHPTextView.OnChangeHP)
            .AddTo(this.gameObject);

        gunManager_model.ReloadValue
            .Subscribe(bulletReloadGageView.OnChangeReloadValue)
            .AddTo(this.gameObject);

        scoreManager_model.KillCountReactiveProperty
            .Subscribe(killCountTextView.OnChangeKillCount)
            .AddTo(this.gameObject);
    }

}
