using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using EntranceTransition;
using VContainer;

public class EntranceUIPresenter : MonoBehaviour
{
    [Header("メインメニュー")]
    [SerializeField] StageSelectButtonView stageSelectButtonView;
    [SerializeField] OptionButtonView optionButtonView;
    [SerializeField] DecisionStageButtonView decisionStageButtonView;
    [SerializeField] BackMainMenuButtonView[] backMainMenuButtonViews;
    
    [Space(20), Header("ステージセレクト")]
    [SerializeField] StageImageBackView stageImageBackView;
    [SerializeField] StageDetailView stageDetailView;
    [SerializeField] List<StageElementView> stageElementViews;

    ScoreHolder scoreHolder;

    [Inject]
    public void Construct(ScoreHolder sholder)
    {
        scoreHolder = sholder;
    }

    // Awakeの方がいい？
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
        stageSelectButtonView.OnStageSelectButtonClickedListener += () => { OnStageSelectButtonClicked(); };
        // 「オプション」ボタン
        optionButtonView.OnOptionButtonClickedListener += () => { OnOptionButtonClicked(); };
        // 「出撃」ボタン
        decisionStageButtonView.OnDecisionStageButtonClickedListener += () => { OnDecisionStageButtonClicked(); };
        // 「(メインメニューに)戻る」ボタン
        foreach(BackMainMenuButtonView b in backMainMenuButtonViews)
        {
            b.OnBackMainMenuButtonClickedListener += () => { OnBackMainMenuButtonClicked(); };
        }
    }

    private void Bind()
    {
        // ステージデータ →
        scoreHolder.StageDataReactiveProperty
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

    //============ Events ============== 

    /// <summary>
    /// ステージ(アイテム)ボタンがクリックされたとき
    /// </summary>
    private void OnStageItemButtonClicked(StageDetailData data)
    {
        scoreHolder.StageData = data;
    }

    /// <summary>
    /// ステージセレクトボタンがクリックされたとき
    /// </summary>
    private void OnStageSelectButtonClicked()
    {
        EntranceManager.Instance.SetMenuStatus(MenuStatus.StageSelect);
    }

    /// <summary>
    /// オプションボタンがクリックされたとき
    /// </summary>
    private void OnOptionButtonClicked()
    {
        EntranceManager.Instance.SetMenuStatus(MenuStatus.Option);
    }

    /// <summary>
    /// 出撃ボタンがクリックされたときが
    /// </summary>
    private void OnDecisionStageButtonClicked()
    {
        EntranceManager.Instance.SetMenuStatus(MenuStatus.Sortie);
    }

    /// <summary>
    /// メインメニューに戻るボタンがクリックされたとき
    /// </summary>
    private void OnBackMainMenuButtonClicked()
    {
        EntranceManager.Instance.SetMenuStatus(MenuStatus.MainMenu);
    }
}