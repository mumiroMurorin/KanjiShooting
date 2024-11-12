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
    [SerializeField] WaveTextView waveTextView;
    [SerializeField] TimerTextView timerTextView;
    [SerializeField] DamageEffectView damageEffectView;
    [SerializeField] PreviousWaveView previousWaveView;
    [SerializeField] NextWaveView nextWaveView;
    [SerializeField] AnswerBoxSpawner[] answerBoxSpawners;

    [SerializeField] SerializeInterface<IStatus> playerStatus_model;
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
        // プレイヤーHP → HPバー
        playerStatus_model.Value.HPNormalized
            .Subscribe(playerHPBarView.OnChangeHP)
            .AddTo(this.gameObject);

        // プレイヤーHP → ダメージ時画面効果
        playerStatus_model.Value.HPNormalized
            .Pairwise()
            .Where(pair => pair.Current < pair.Previous) // 減少しているときのみ
            .Subscribe(pair => damageEffectView.OnDamage(pair.Previous - pair.Current))
            .AddTo(this.gameObject);

        // プレイヤーHP → HPテキスト
        playerStatus_model.Value.HP
            .Subscribe(playerHPTextView.OnChangeHP)
            .AddTo(this.gameObject);

        // 弾リロード時間 → リロードUI
        gunManager_model.ReloadValue
            .Subscribe(bulletReloadGageView.OnChangeReloadValue)
            .AddTo(this.gameObject);

        // キルカウントスコア → キルカウントテキスト
        ScoreManager.Instance.KillCountReactiveProperty
            .Subscribe(killCountTextView.OnChangeKillCount)
            .AddTo(this.gameObject);

        // Waveカウント → 経過Waveテキスト
        ScoreManager.Instance.WaveCountReactiveProperty
            .Subscribe(waveTextView.OnChangeWaveCount)
            .AddTo(this.gameObject);

        ScoreManager.Instance.WaveCountReactiveProperty
            .Subscribe(count => 
            {
                previousWaveView.OnChangePreviousWave(count);
                nextWaveView.OnChangePreviousWave(count);
            })
            .AddTo(this.gameObject);

        // 経過時間 → タイマーテキスト
        ScoreManager.Instance.TimeCountreactiveProperty
            .Subscribe(timerTextView.OnChangeTimeCount)
            .AddTo(this.gameObject);

        // 回答 → 解答ログ
        ScoreManager.Instance.AnswerStatesReactiveCollection
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
