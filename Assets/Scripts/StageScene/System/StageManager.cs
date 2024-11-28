using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using StageTransition;
using VContainer;

public class StageManager : LocalSingletonMonoBehaviour<StageManager>
{
    const StageSceneTag FIRST_STAGESCENE = StageSceneTag.Fighting;
    const StageStatus FIRST_STAGESTATUS = StageStatus.Loading;

    [SerializeField] Transform playerTransform;
    [SerializeField] TimelineManager timelineManager;
    [SerializeField] TextAsset kanjiCsv;
    [SerializeField] WaveManager[] waves;

    // シーンステータス変更時のコールバック登録
    public IReadOnlyReactiveProperty<StageStatus> CurrentStageStatusreactiveproperty { get { return currentStageStatus; } }

    public TextAsset KanjiCSV { get { return kanjiCsv; } }

    StageSceneTransitionManager sceneTransitionManager;
    ReactiveProperty<StageStatus> currentStageStatus;

    ScoreHolder scoreHolder;

    [Inject]
    public void Construct(ScoreHolder holder)
    {
        scoreHolder = holder;
    }

    private new void Awake()
    {
        Initialize();
        SetPhaseTransitioner();
        StartStage();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Initialize()
    {
        sceneTransitionManager = new StageSceneTransitionManager();
        currentStageStatus = new ReactiveProperty<StageStatus>(FIRST_STAGESTATUS);

        //各ウェーブの初期化
        foreach (WaveManager w in waves)
        {
            w.Initialize(playerTransform);
        }

        //スコアの初期化
        scoreHolder.KillCount = 0;
        scoreHolder.TimeCount = 0;
        scoreHolder.WaveCount = 0;
        scoreHolder.ResetAnswerStatus();
    }

    /// <summary>
    /// 遷移関数の追加(処理はここに追加)
    /// </summary>
    private void SetPhaseTransitioner()
    {
        List<IPhaseTransitioner> stagePhaseTransitioners = new List<IPhaseTransitioner>();

        // 通常のゲームループ
        // ステージ開始演出
        stagePhaseTransitioners.Add(new SceneStartTransition(timelineManager.GetPlayableDirector("LoadFinish")));
        stagePhaseTransitioners.Add(new StageStartEffectTransition(timelineManager.GetPlayableDirector("StageStart")));
        
        //WAVE内ループ
        foreach (WaveManager w in waves)
        {
            // ウェーブ開始演出
            stagePhaseTransitioners.Add(new WaveStartEffectTransition(timelineManager.GetPlayableDirector("WaveStart"), scoreHolder));
            // ウェーブ開始
            stagePhaseTransitioners.Add(new WaveStartTransition(w));
            // ウェーブ終了処理開始
            stagePhaseTransitioners.Add(new WaveFinishTransition(w));
            // ウェーブ終了演出
            stagePhaseTransitioners.Add(new WaveFinishEffectTransition(timelineManager.GetPlayableDirector("WaveFinish")));
        }

        // ステージ終了
        stagePhaseTransitioners.Add(new StageFinishTransition(scoreHolder));
        // ステージ終了演出
        stagePhaseTransitioners.Add(new StageFinishEffectTransition(timelineManager.GetPlayableDirector("StageFinish")));
        // シーンチェンジ
        stagePhaseTransitioners.Add(new ResultSceneTransition(timelineManager.GetPlayableDirector("Result")));

        //シーンの追加
        sceneTransitionManager.AddPhaseTransitioner(StageSceneTag.Fighting, GenerateScenePhaseTransitionManager(stagePhaseTransitioners));
        stagePhaseTransitioners = new List<IPhaseTransitioner>();

        // ゲームオーバー
        // ゲームオーバー処理開始
        stagePhaseTransitioners.Add(new StageFailedStartTransition(scoreHolder));
        // ゲームオーバー演出開始
        stagePhaseTransitioners.Add(new StageFailedStartEffectTransition(timelineManager.GetPlayableDirector("StageFailedStart")));
        // シーンチェンジ
        stagePhaseTransitioners.Add(new ResultSceneTransition(timelineManager.GetPlayableDirector("Result")));

        //シーンの追加
        sceneTransitionManager.AddPhaseTransitioner(StageSceneTag.StageFailed, GenerateScenePhaseTransitionManager(stagePhaseTransitioners));
        stagePhaseTransitioners = new List<IPhaseTransitioner>();

    }

    /// <summary>
    /// 解答ステータスの登録
    /// </summary>
    /// <param name="status"></param>
    public void AddAnswerStatus(AnswerStatus status)
    {
        // ログの生成
        string log = "";
        switch (status.state)
        {
            case AnswerState.Corrected:
                log += "【解答】 〇正解";
                break;
            case AnswerState.Incorrected:
                log += "【解答】 ×不正解";
                break;
            case AnswerState.CollateralDamage:
                log += "【解答】 〇巻き添え";
                break;
        }

        log += $" 正答: {string.Join(",", status.questionData.answers)}";
        Debug.Log(log);

        scoreHolder.AddAnswerStatus(status);
    }

    /// <summary>
    /// ScenePhaseTransitionManagerを生成する
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
    /// ステージの開始
    /// </summary>
    /// <param name="token"></param>
    private void StartStage()
    {
        sceneTransitionManager.ExecuteStageScene(FIRST_STAGESCENE);
    }

    /// <summary>
    /// ステージシーンの遷移処理
    /// </summary>
    /// <param name="tag"></param>
    public void ChangeStageScene(StageSceneTag tag)
    {
        Debug.Log($"【System】シーン遷移: {tag}");
        sceneTransitionManager.ExecuteStageScene(tag);
    }

    /// <summary>
    /// ステージステータスの変更
    /// </summary>
    public void ChangeStageStatus(StageStatus status)
    {
        Debug.Log($"【System】ステータス変更: {status}");
        currentStageStatus.Value = status; 
    }

    private void OnDestroy()
    {
        sceneTransitionManager.OnDestroy();
    }
}