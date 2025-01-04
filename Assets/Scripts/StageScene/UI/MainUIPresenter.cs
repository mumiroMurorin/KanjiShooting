using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;
using StageUI;

public class MainUIPresenter : MonoBehaviour
{
    [SerializeField] PlayerHPBarView playerHPBarView;
    [SerializeField] PlayerHPTextView playerHPTextView;
    [SerializeField] BulletReloadGageView bulletReloadGageView;
    [SerializeField] ExplosionChargeGageView explosionChargeGageView;
    [SerializeField] SpecialChargeGaugeView specialChargeGaugeView;
    [SerializeField] SpecialActionGuideView specialActionGuideView;
    [SerializeField] KillCountTextView killCountTextView;
    [SerializeField] StageDataView stageDataView;
    [SerializeField] WaveTextView waveTextView;
    [SerializeField] TimerTextView timerTextView;
    [SerializeField] DamageEffectView damageEffectView;
    [SerializeField] PreviousWaveView previousWaveView;
    [SerializeField] NextWaveView nextWaveView;
    [SerializeField] BackGameButtonView backGameButtonView;
    [SerializeField] BackMainMenuButtonView backMainMenuButtonView;
    [SerializeField] AnswerBoxSpawner[] answerBoxSpawners;

    [SerializeField] SerializeInterface<IStatus> playerStatus_model;
    [SerializeField] GunManager gunManager_model;
    [SerializeField] SerializeInterface<IBulletReloadCharger> bulletReloadCharger_model;
    [SerializeField] SerializeInterface<IBulletShootCharger> explosionCharger_model;
    [SerializeField] SerializeInterface<IJapaneseInputHolder> inputHolder;

    ScoreHolder scoreHolder;
    OptionHolder optionHolder;

    [Inject]
    public void Construct(ScoreHolder scoreHolder, OptionHolder optionHolder)
    {
        this.scoreHolder = scoreHolder;
        this.optionHolder = optionHolder;
    }

    private void Start()
    {
        SetEvents();
        Bind();
    }

    private void SetEvents()
    {
        // 「ゲームに戻る」ボタン
        backGameButtonView.OnBackGameButtonClickedListener += () => StageManager.Instance.ResumeTime();

        // 「ステージ中断」ボタン
        backMainMenuButtonView.OnBackMainMenuButtonClickedListener += () => OnBackMainMenuButtonPushed();
    }

    private void Bind()
    {
        // プレイヤーHP → HPバー
        playerStatus_model?.Value.HPNormalized
            .Subscribe(playerHPBarView.OnChangeHP)
            .AddTo(this.gameObject);

        // プレイヤーHP → ダメージ時画面効果
        playerStatus_model?.Value.HPNormalized
            .Pairwise()
            .Where(pair => pair.Current < pair.Previous) // 減少しているときのみ
            .Subscribe(pair => damageEffectView.OnDamage(pair.Previous - pair.Current))
            .AddTo(this.gameObject);

        // プレイヤーHP → HPテキスト
        playerStatus_model?.Value.HP
            .Subscribe(playerHPTextView.OnChangeHP)
            .AddTo(this.gameObject);

        // プレイヤーHP → HPテキスト色
        playerStatus_model?.Value.HPNormalized
            .Subscribe(playerHPTextView.OnChangeHP)
            .AddTo(this.gameObject);

        // 弾リロード時間 → リロードUI
        gunManager_model?.ReloadValue
            .Subscribe(bulletReloadGageView.OnChangeReloadValue)
            .AddTo(this.gameObject);

        // 自爆チャージ時間 → チャージUI
        explosionCharger_model?.Value.ChargeCount
            .Subscribe(explosionChargeGageView.OnChangeChargeValue)
            .AddTo(this.gameObject);

        // スペシャル弾チャージ → チャージUI
        bulletReloadCharger_model?.Value.ChargeCount
            .Subscribe(specialChargeGaugeView.OnChangeChargeValue)
            .AddTo(this.gameObject);

        // スペシャル弾チャージ → アクションガイドUI
        bulletReloadCharger_model?.Value.ChargeCount
            .Subscribe(specialActionGuideView.OnChangeChargeValue)
            .AddTo(this.gameObject);

        inputHolder.Value.AnswerReactiveProperty
            .Subscribe(answer => specialActionGuideView.OnChangeActionMode(answer.Length > 0))
            .AddTo(this.gameObject);

        // キルカウントスコア → キルカウントテキスト
        scoreHolder.KillCountReactiveProperty
            .Subscribe(killCountTextView.OnChangeKillCount)
            .AddTo(this.gameObject);

        // ステージデータ → ステージタイトル他
        scoreHolder.StageDataReactiveProperty
            .Subscribe(stageDataView.OnChangeStageData)
            .AddTo(this.gameObject);

        // Waveカウント → 経過Waveテキスト
        scoreHolder.WaveCountReactiveProperty
            .Subscribe(waveTextView.OnChangeWaveCount)
            .AddTo(this.gameObject);

        // Waveカウント → Wave遷移アニメーション
        scoreHolder.WaveCountReactiveProperty
            .Subscribe(count => 
            {
                previousWaveView.OnChangePreviousWave(count);
                nextWaveView.OnChangePreviousWave(count);
            })
            .AddTo(this.gameObject);

        // 経過時間 → タイマーテキスト
        scoreHolder.TimeCountReactiveProperty
            .Subscribe(timerTextView.OnChangeTimeCount)
            .AddTo(this.gameObject);

        // 回答 → 解答ログ
        scoreHolder.AnswerStatesReactiveCollection
            .ObserveAdd().Subscribe(addState => SpawnAnswerBox(addState.Value))
            .AddTo(this);
    }

    /// <summary>
    /// 条件に合ったスポナーでアンサーボックスをスポーンさせる
    /// </summary>
    /// <param name="answerState"></param>
    private void SpawnAnswerBox(AnswerStatus answerStatus)
    {
        if (!optionHolder.AnswerDisplayValidityReactiveProperty.Value) { return; }
        
        answerStatus.answerDisplayTime = optionHolder.AnswerDispleyTime.Value;
        foreach (AnswerBoxSpawner a in answerBoxSpawners)
        {
            if (a.CanSpawn(answerStatus)) 
            { 
                a.Spawn(answerStatus);
                break;
            }
        }
    }

    /// <summary>
    /// 「メインメニューに戻る」ボタンがクリックされたとき
    /// </summary>
    private void OnBackMainMenuButtonPushed()
    {
        StageManager.Instance.ChangeStageScene(StageTransition.StageSceneTag.Interrupt);
    }

}
