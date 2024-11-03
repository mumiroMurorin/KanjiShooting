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
    [SerializeField] AnswerBoxSpawner[] answerBoxSpawners;

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
        //プレイヤーHP → HPバー
        playerStatus_model.Value.HPNormalized
            .Subscribe(playerHPBarView.OnChangeHP)
            .AddTo(this.gameObject);

        //プレイヤーHP → ダメージ時画面効果
        playerStatus_model.Value.HPNormalized
            .Pairwise()
            .Where(pair => pair.Current < pair.Previous) // 減少しているときのみ
            .Subscribe(pair => damageEffectView.OnDamage(pair.Previous - pair.Current))
            .AddTo(this.gameObject);

        //プレイヤーHP → HPテキスト
        playerStatus_model.Value.HP
            .Subscribe(playerHPTextView.OnChangeHP)
            .AddTo(this.gameObject);

        //弾リロード時間 → リロードUI
        gunManager_model.ReloadValue
            .Subscribe(bulletReloadGageView.OnChangeReloadValue)
            .AddTo(this.gameObject);

        //キルカウントスコア → キルカウントテキスト
        scoreManager_model.KillCountReactiveProperty
            .Subscribe(killCountTextView.OnChangeKillCount)
            .AddTo(this.gameObject);

        //回答 → 解答ログ
        scoreManager_model.AnswerStatesReactiveCollection
            .ObserveAdd().Subscribe(addState => SpawnAnswerBox(addState.Value))
            .AddTo(this);
    }

    /// <summary>
    /// 条件に合ったスポナーでアンサーボックスをスポーンさせる
    /// </summary>
    /// <param name="answerState"></param>
    private void SpawnAnswerBox(AnswerState answerState)
    {
        foreach(AnswerBoxSpawner a in answerBoxSpawners)
        {
            if (a.CanSpawn(answerState)) 
            { 
                a.Spawn(answerState);
                break;
            }
        }
    }
}
