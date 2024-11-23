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
