using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using EntranceTransition;

public class EntranceUIPresenter : MonoBehaviour
{
    [SerializeField] StageSelectButtonView stageSelectButtonView;
    [SerializeField] OptionButtonView optionButtonView;
    [SerializeField] DecisionStageButtonView decisionStageButtonView;
    [SerializeField] BackMainMenuButtonView backMainMenuButtonView;

    [SerializeField] StageImageBackView stageImageBackView;
    [SerializeField] StageDetailView stageDetailView;
    [SerializeField] List<StageElementView> stageElementViews;

    [SerializeField] ScoreManager scoreManager_model;

    private void Start()
    {
        SetEvents();
        Bind();
    }

    private void SetEvents()
    {
        // ステージボタンが押されたときの処理
        foreach (StageElementView view in stageElementViews)
        {
            view.OnStageItemButtonClickedListener += OnStageItemButtonClicked;
        }

        // 「ステージ選択」ボタン
        stageSelectButtonView.OnStageSelectButtonClickedListener += () => { EntranceManager.Instance.SetMenuStatus(MenuStatus.StageSelect); };
        // 「オプション」ボタン
        optionButtonView.OnOptionButtonClickedListener += () => { EntranceManager.Instance.SetMenuStatus(MenuStatus.Option); };
        // 「出撃」ボタン
        decisionStageButtonView.OnDecisionStageButtonClickedListener += () => { EntranceManager.Instance.SetMenuStatus(MenuStatus.Sortie); };
        // 「(メインメニューに)戻る」ボタン
        backMainMenuButtonView.OnBackMainMenuButtonClickedListener += () => { EntranceManager.Instance.SetMenuStatus(MenuStatus.MainMenu); };
    }

    private void Bind()
    {
        // ステージデータ →
        scoreManager_model.StageDataReactiveProperty
            .Where(data => data != null)
            .Subscribe(data => 
            {
                // 背景
                stageImageBackView.OnSelectStage(data.BackGroundSprite);
                // ステージ詳細
                stageDetailView.OnSelectStage(data);
            })
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// ステージ(アイテム)ボタンがクリックされたとき
    /// </summary>
    private void OnStageItemButtonClicked(StageDetailData data)
    {
        scoreManager_model.StageData = data;
    }

}