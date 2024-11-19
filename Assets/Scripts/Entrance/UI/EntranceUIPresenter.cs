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
        // �X�e�[�W�{�^���������ꂽ�Ƃ��̏���
        foreach (StageElementView view in stageElementViews)
        {
            view.OnStageItemButtonClickedListener += OnStageItemButtonClicked;
        }

        // �u�X�e�[�W�I���v�{�^��
        stageSelectButtonView.OnStageSelectButtonClickedListener += () => { EntranceManager.Instance.SetMenuStatus(MenuStatus.StageSelect); };
        // �u�I�v�V�����v�{�^��
        optionButtonView.OnOptionButtonClickedListener += () => { EntranceManager.Instance.SetMenuStatus(MenuStatus.Option); };
        // �u�o���v�{�^��
        decisionStageButtonView.OnDecisionStageButtonClickedListener += () => { EntranceManager.Instance.SetMenuStatus(MenuStatus.Sortie); };
        // �u(���C�����j���[��)�߂�v�{�^��
        backMainMenuButtonView.OnBackMainMenuButtonClickedListener += () => { EntranceManager.Instance.SetMenuStatus(MenuStatus.MainMenu); };
    }

    private void Bind()
    {
        // �X�e�[�W�f�[�^ ��
        scoreManager_model.StageDataReactiveProperty
            .Where(data => data != null)
            .Subscribe(data => 
            {
                // �w�i
                stageImageBackView.OnSelectStage(data.BackGroundSprite);
                // �X�e�[�W�ڍ�
                stageDetailView.OnSelectStage(data);
            })
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// �X�e�[�W(�A�C�e��)�{�^�����N���b�N���ꂽ�Ƃ�
    /// </summary>
    private void OnStageItemButtonClicked(StageDetailData data)
    {
        scoreManager_model.StageData = data;
    }

}