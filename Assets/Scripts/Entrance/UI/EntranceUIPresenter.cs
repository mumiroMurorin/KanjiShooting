using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using EntranceTransition;
using VContainer;

public class EntranceUIPresenter : MonoBehaviour
{
    [Header("���C�����j���[")]
    [SerializeField] StageSelectButtonView stageSelectButtonView;
    [SerializeField] OptionButtonView optionButtonView;
    [SerializeField] DecisionStageButtonView decisionStageButtonView;
    [SerializeField] BackMainMenuButtonView[] backMainMenuButtonViews;
    
    [Space(20), Header("�X�e�[�W�Z���N�g")]
    [SerializeField] StageImageBackView stageImageBackView;
    [SerializeField] StageDetailView stageDetailView;
    [SerializeField] List<StageElementView> stageElementViews;

    ScoreHolder scoreHolder;

    [Inject]
    public void Construct(ScoreHolder sholder)
    {
        scoreHolder = sholder;
    }

    // Awake�̕��������H
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
        stageSelectButtonView.OnStageSelectButtonClickedListener += () => { OnStageSelectButtonClicked(); };
        // �u�I�v�V�����v�{�^��
        optionButtonView.OnOptionButtonClickedListener += () => { OnOptionButtonClicked(); };
        // �u�o���v�{�^��
        decisionStageButtonView.OnDecisionStageButtonClickedListener += () => { OnDecisionStageButtonClicked(); };
        // �u(���C�����j���[��)�߂�v�{�^��
        foreach(BackMainMenuButtonView b in backMainMenuButtonViews)
        {
            b.OnBackMainMenuButtonClickedListener += () => { OnBackMainMenuButtonClicked(); };
        }
    }

    private void Bind()
    {
        // �X�e�[�W�f�[�^ ��
        scoreHolder.StageDataReactiveProperty
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

    //============ Events ============== 

    /// <summary>
    /// �X�e�[�W(�A�C�e��)�{�^�����N���b�N���ꂽ�Ƃ�
    /// </summary>
    private void OnStageItemButtonClicked(StageDetailData data)
    {
        scoreHolder.StageData = data;
    }

    /// <summary>
    /// �X�e�[�W�Z���N�g�{�^�����N���b�N���ꂽ�Ƃ�
    /// </summary>
    private void OnStageSelectButtonClicked()
    {
        EntranceManager.Instance.SetMenuStatus(MenuStatus.StageSelect);
    }

    /// <summary>
    /// �I�v�V�����{�^�����N���b�N���ꂽ�Ƃ�
    /// </summary>
    private void OnOptionButtonClicked()
    {
        EntranceManager.Instance.SetMenuStatus(MenuStatus.Option);
    }

    /// <summary>
    /// �o���{�^�����N���b�N���ꂽ�Ƃ���
    /// </summary>
    private void OnDecisionStageButtonClicked()
    {
        EntranceManager.Instance.SetMenuStatus(MenuStatus.Sortie);
    }

    /// <summary>
    /// ���C�����j���[�ɖ߂�{�^�����N���b�N���ꂽ�Ƃ�
    /// </summary>
    private void OnBackMainMenuButtonClicked()
    {
        EntranceManager.Instance.SetMenuStatus(MenuStatus.MainMenu);
    }
}