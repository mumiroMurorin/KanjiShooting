using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    const StageSceneTag FIRST_STAGESCENE = StageSceneTag.Fighting;
    const StageStatus FIRST_STAGESTATUS = StageStatus.Loading;

    [SerializeField] Transform playerTransform;
    [SerializeField] TimelineManager timelineManager;
    [SerializeField] TextAsset kanjiCsv;
    [SerializeField] WaveManager[] waves;

    // �V�[���X�e�[�^�X�ύX���̃R�[���o�b�N�o�^
    public IReactiveProperty<StageStatus> CurrentStageStatusreactiveproperty { get { return currentStageStatus; } }

    public TextAsset KanjiCSV { get { return kanjiCsv; } }

    StageSceneTransitionManager sceneTransitionManager;
    ReactiveProperty<StageStatus> currentStageStatus;

    private void Start()
    {
        Initialize();
        SetPhaseTransitioner();
        StartStage();
    }

    new private void Awake()
    {
        //�I�u�U�[�o�[�̏�����
        currentStageStatus = new ReactiveProperty<StageStatus>(FIRST_STAGESTATUS);
    }

    /// <summary>
    /// ������
    /// </summary>
    private void Initialize()
    {
        sceneTransitionManager = new StageSceneTransitionManager();

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
        List<IStagePhaseTransitioner> stagePhaseTransitioners = new List<IStagePhaseTransitioner>();

        //�ʏ�̃Q�[�����[�v
        //�X�e�[�W�J�n���o
        stagePhaseTransitioners.Add(new StageStartEffectTransition(timelineManager.GetStageStartPlayableDirector()));
        
        //WAVE�����[�v
        foreach (WaveManager w in waves)
        {
            //�E�F�[�u�J�n���o
            stagePhaseTransitioners.Add(new WaveStartEffectTransition(timelineManager.GetWaveStartPlayableDirector()));
            //�E�F�[�u�J�n
            stagePhaseTransitioners.Add(new WaveStartTransition(w));
            //�E�F�[�u�I�������J�n
            stagePhaseTransitioners.Add(new WaveFinishTransition(w));
            //�E�F�[�u�I�����o
            stagePhaseTransitioners.Add(new WaveFinishEffectTransition(timelineManager.GetWaveFinishPlayableDirector()));
        }

        //�X�e�[�W�I��
        stagePhaseTransitioners.Add(new StageFinishTransition());
        //�X�e�[�W�I�����o
        stagePhaseTransitioners.Add(new StageFinishEffectTransition(timelineManager.GetStageFinishPlayableDirector()));

        //�V�[���̒ǉ�
        sceneTransitionManager.AddPhaseTransitioner(StageSceneTag.Fighting, GenerateScenePhaseTransitionManager(stagePhaseTransitioners));
        stagePhaseTransitioners = new List<IStagePhaseTransitioner>();

        //�Q�[���I�[�o�[
        //�Q�[���I�[�o�[�����J�n
        stagePhaseTransitioners.Add(new StageFailedStartTransition());
        //�Q�[���I�[�o�[���o�J�n
        stagePhaseTransitioners.Add(new StageFailedStartEffectTransition(timelineManager.GetStageFailedStartPlayableDirector()));
        //�Q�[���I�[�o�[��ʏI�����o
        stagePhaseTransitioners.Add(new StageFailedFinishEffectTransition(timelineManager.GetStageFailedFinishPlayableDirector()));
        //�Q�[���I�[�o�[�����I��
        stagePhaseTransitioners.Add(new StageFailedFinishTransition());

        //�V�[���̒ǉ�
        sceneTransitionManager.AddPhaseTransitioner(StageSceneTag.StageFailed, GenerateScenePhaseTransitionManager(stagePhaseTransitioners));
        stagePhaseTransitioners = new List<IStagePhaseTransitioner>();

    }

    /// <summary>
    /// ScenePhaseTransitionManager�𐶐�����
    /// </summary>
    /// <param name="stagePhaseTransitioners"></param>
    /// <returns></returns>
    private ScenePhaseTransitionManager GenerateScenePhaseTransitionManager(List<IStagePhaseTransitioner> stagePhaseTransitioners)
    {
        ScenePhaseTransitionManager transitionManager = new ScenePhaseTransitionManager();
        foreach (IStagePhaseTransitioner s in stagePhaseTransitioners)
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

public enum StageSceneTag
{
    Fighting,
    StageFailed
}

public enum StageStatus
{
    Loading,
    Battling,
    StageFinish,
}

/// <summary>
/// �X�e�[�W���V�[���̊Ǘ��N���X
/// </summary>
public class StageSceneTransitionManager
{
    readonly Dictionary<StageSceneTag, ScenePhaseTransitionManager> stagePhases = new Dictionary<StageSceneTag, ScenePhaseTransitionManager>();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    /// <summary>
    /// �X�e�[�W���V�[���̒ǉ�
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="phaseTransitionManager"></param>
    public void AddPhaseTransitioner(StageSceneTag sceneTag, ScenePhaseTransitionManager phaseTransitionManager)
    {
        if(!stagePhases.TryAdd(sceneTag, phaseTransitionManager))
        {
            Debug.LogWarning($"�ySystem�zSceneTag:{sceneTag} �ɑΉ�����X�e�[�W���V�[���͊��ɓo�^����Ă��܂�");
            return;
        }
    }

    /// <summary>
    /// �V�[���̑J�ځA�L�����Z������
    /// </summary>
    /// <param name="sceneTag"></param>
    public void ExecuteStageScene(StageSceneTag sceneTag)
    {
        if(!stagePhases.TryGetValue(sceneTag,out ScenePhaseTransitionManager manager))
        {
            Debug.LogError($"�ySystem�z�X�e�[�W���V�[�����o�^����Ă��܂���: {sceneTag}");
            return;
        }

        //�X�e�[�W�V�[���̕ύX
        //���ݎ��s���̃V�[�P���X�𒆒f�A���Z�b�g
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
        cancellationTokenSource = new CancellationTokenSource();

        manager.ExecuteAsync(cancellationTokenSource.Token).Forget();
    }

    public void OnDestroy()
    {
        cancellationTokenSource.Cancel();
    }
}

/// <summary>
/// �X�e�[�W�V�[�����t�F�[�Y�̑J�ڂ�S���N���X
/// </summary>
public class ScenePhaseTransitionManager
{
    readonly List<IStagePhaseTransitioner> transitions = new List<IStagePhaseTransitioner>();

    /// <summary>
    /// �J�ڏ����̒ǉ�
    /// </summary>
    /// <param name="transitioner"></param>
    public void AddTransition(IStagePhaseTransitioner transitioner)
    {
        transitions.Add(transitioner);
    }

    /// <summary>
    /// �V�[�P���X�̎��s
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async UniTask ExecuteAsync(CancellationToken cancellationToken)
    {
        Debug.Log("�ySystem�z�X�e�[�W���V�[���J�n");
        foreach (var transition in transitions)
        {
            await transition.ExecuteAsync(cancellationToken);
        }
        Debug.Log("�ySystem�z�X�e�[�W���V�[���I��");
    }
}