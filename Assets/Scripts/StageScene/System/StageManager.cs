using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using StageTransition;

public class StageManager : LocalSingletonMonoBehaviour<StageManager>
{
    const StageSceneTag FIRST_STAGESCENE = StageSceneTag.Fighting;
    const StageStatus FIRST_STAGESTATUS = StageStatus.Loading;

    [SerializeField] Transform playerTransform;
    [SerializeField] TimelineManager timelineManager;
    [SerializeField] TextAsset kanjiCsv;
    [SerializeField] WaveManager[] waves;

    // �V�[���X�e�[�^�X�ύX���̃R�[���o�b�N�o�^
    public IReadOnlyReactiveProperty<StageStatus> CurrentStageStatusreactiveproperty { get { return currentStageStatus; } }

    public TextAsset KanjiCSV { get { return kanjiCsv; } }

    StageSceneTransitionManager sceneTransitionManager;
    ReactiveProperty<StageStatus> currentStageStatus;

    private new void Awake()
    {
        Initialize();
        SetPhaseTransitioner();
        StartStage();
    }

    /// <summary>
    /// ������
    /// </summary>
    private void Initialize()
    {
        sceneTransitionManager = new StageSceneTransitionManager();
        currentStageStatus = new ReactiveProperty<StageStatus>(FIRST_STAGESTATUS);

        //�e�E�F�[�u�̏�����
        foreach (WaveManager w in waves)
        {
            w.Initialize(playerTransform);
        }

        //�X�R�A�̏�����
        ScoreManager.Instance.KillCount = 0;
        ScoreManager.Instance.TimeCount = 0;
        ScoreManager.Instance.WaveCount = 0;
    }

    /// <summary>
    /// �J�ڊ֐��̒ǉ�(�����͂����ɒǉ�)
    /// </summary>
    private void SetPhaseTransitioner()
    {
        List<IPhaseTransitioner> stagePhaseTransitioners = new List<IPhaseTransitioner>();

        //�ʏ�̃Q�[�����[�v
        //�X�e�[�W�J�n���o
        stagePhaseTransitioners.Add(new StageStartEffectTransition(timelineManager.GetPlayableDirector("StageStart")));
        
        //WAVE�����[�v
        foreach (WaveManager w in waves)
        {
            //�E�F�[�u�J�n���o
            stagePhaseTransitioners.Add(new WaveStartEffectTransition(timelineManager.GetPlayableDirector("WaveStart")));
            //�E�F�[�u�J�n
            stagePhaseTransitioners.Add(new WaveStartTransition(w));
            //�E�F�[�u�I�������J�n
            stagePhaseTransitioners.Add(new WaveFinishTransition(w));
            //�E�F�[�u�I�����o
            stagePhaseTransitioners.Add(new WaveFinishEffectTransition(timelineManager.GetPlayableDirector("WaveFinish")));
        }

        //�X�e�[�W�I��
        stagePhaseTransitioners.Add(new StageFinishTransition());
        //�X�e�[�W�I�����o
        stagePhaseTransitioners.Add(new StageFinishEffectTransition(timelineManager.GetPlayableDirector("StageFinish")));

        //�V�[���̒ǉ�
        sceneTransitionManager.AddPhaseTransitioner(StageSceneTag.Fighting, GenerateScenePhaseTransitionManager(stagePhaseTransitioners));
        stagePhaseTransitioners = new List<IPhaseTransitioner>();

        //�Q�[���I�[�o�[
        //�Q�[���I�[�o�[�����J�n
        stagePhaseTransitioners.Add(new StageFailedStartTransition());
        //�Q�[���I�[�o�[���o�J�n
        stagePhaseTransitioners.Add(new StageFailedStartEffectTransition(timelineManager.GetPlayableDirector("StageFailedStart")));
        //�Q�[���I�[�o�[��ʏI�����o
        stagePhaseTransitioners.Add(new StageFailedFinishEffectTransition(timelineManager.GetPlayableDirector("StageFailedFinish")));
        //�Q�[���I�[�o�[�����I��
        stagePhaseTransitioners.Add(new StageFailedFinishTransition());

        //�V�[���̒ǉ�
        sceneTransitionManager.AddPhaseTransitioner(StageSceneTag.StageFailed, GenerateScenePhaseTransitionManager(stagePhaseTransitioners));
        stagePhaseTransitioners = new List<IPhaseTransitioner>();

    }

    /// <summary>
    /// ScenePhaseTransitionManager�𐶐�����
    /// </summary>
    /// <param name="stagePhaseTransitioners"></param>
    /// <returns></returns>
    private ScenePhaseTransitionManager GenerateScenePhaseTransitionManager(List<IPhaseTransitioner> stagePhaseTransitioners)
    {
        ScenePhaseTransitionManager transitionManager = new ScenePhaseTransitionManager();
        foreach (IPhaseTransitioner s in stagePhaseTransitioners)
        {
            transitionManager.AddTransition(s);
        }
        return transitionManager;
    }

    /// <summary>
    /// �X�e�[�W�̊J�n
    /// </summary>
    /// <param name="token"></param>
    private void StartStage()
    {
        sceneTransitionManager.ExecuteStageScene(FIRST_STAGESCENE);
    }

    /// <summary>
    /// �X�e�[�W�V�[���̑J�ڏ���
    /// </summary>
    /// <param name="tag"></param>
    public void ChangeStageScene(StageSceneTag tag)
    {
        Debug.Log($"�ySystem�z�V�[���J��: {tag}");
        sceneTransitionManager.ExecuteStageScene(tag);
    }

    /// <summary>
    /// �X�e�[�W�X�e�[�^�X�̕ύX
    /// </summary>
    public void ChangeStageStatus(StageStatus status)
    {
        Debug.Log($"�ySystem�z�X�e�[�^�X�ύX: {status}");
        currentStageStatus.Value = status; 
    }

    private void OnDestroy()
    {
        sceneTransitionManager.OnDestroy();
    }
}