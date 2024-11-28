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
        // �V�[���J�� �� �A�j���[�V�����̍Đ�
        ResultManager.Instance.CurentStatusReactiveProperty
            .Where(status => status == MenuStatus.Result)
            .Subscribe(_ => answerView.OnTransitionResult(ReactiveCollectionToArray(scoreHolder.AnswerStatesReactiveCollection)))
            .AddTo(this.gameObject);

        // �L�^�^�C�� �� UI
        scoreHolder.TimeCountReactiveProperty
            .Subscribe(timeScoreView.OnChangeScore)
            .AddTo(this.gameObject);

        // ������ �� UI
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
    /// �u���C�����j���[�ɖ߂�v�{�^�����N���b�N���ꂽ�Ƃ�
    /// </summary>
    private void OnBackMainMenuButtonPushed()
    {
        ResultManager.Instance.SetMenuStatus(MenuStatus.BackEntrance);
    }

    /// <summary>
    /// �u���g���C�v�{�^�����N���b�N���ꂽ�Ƃ�
    /// </summary>
    private void OnRetryButtonPushed()
    {
        ResultManager.Instance.SetMenuStatus(MenuStatus.Retry);
    }
}
