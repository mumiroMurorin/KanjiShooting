using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ResultTransition;
using ResultUI;
using VContainer;

public class ResultUIPresenter : MonoBehaviour
{
    [SerializeField] BackMainMenuButtonView backMainMenuButtonView;
    [SerializeField] RetryButtonView retryButtonView;
    [SerializeField] AnswerAnimationView answerView;
    [SerializeField] TimeScoreView timeScoreView;
    [SerializeField] KillScoreView killScoreView;

    ScoreHolder scoreHolder;

    [Inject]
    public void Construct(ScoreHolder sholder)
    {
        scoreHolder = sholder;
    }

    private void Start()
    {
        SetEvents();
        Bind();
    }

    private void SetEvents()
    {
        backMainMenuButtonView.OnBackMainMenuButtonClickedListener += () => { OnBackMainMenuButtonPushed(); };
        retryButtonView.OnRetryButtonClickedListener += () => { OnRetryButtonPushed(); };
    }

    private void Bind()
    {
        // シーン遷移 → アニメーションの再生
        ResultManager.Instance.CurentStatusReactiveProperty
            .Where(status => status == MenuStatus.Result)
            .Subscribe(_ => answerView.OnTransitionResult(ReactiveCollectionToArray(scoreHolder.AnswerStatesReactiveCollection)))
            .AddTo(this.gameObject);

        // 記録タイム → UI
        scoreHolder.TimeCountReactiveProperty
            .Subscribe(timeScoreView.OnChangeScore)
            .AddTo(this.gameObject);

        // 討伐数 → UI
        scoreHolder.KillCountReactiveProperty
            .Subscribe(killScoreView.OnChangeScore)
            .AddTo(this.gameObject);
    }

    private AnswerStatus[] ReactiveCollectionToArray(IReadOnlyReactiveCollection<AnswerStatus> statuses)
    {
        AnswerStatus[] answers = new AnswerStatus[statuses.Count];
        for (int i = 0; i < statuses.Count; i++)
        {
            answers[i] = statuses[i];
        }
        return answers;
    }

    // =================== Events ===================

    /// <summary>
    /// 「メインメニューに戻る」ボタンがクリックされたとき
    /// </summary>
    private void OnBackMainMenuButtonPushed()
    {
        ResultManager.Instance.SetMenuStatus(MenuStatus.BackEntrance);
    }

    /// <summary>
    /// 「リトライ」ボタンがクリックされたとき
    /// </summary>
    private void OnRetryButtonPushed()
    {
        ResultManager.Instance.SetMenuStatus(MenuStatus.Retry);
    }
}
