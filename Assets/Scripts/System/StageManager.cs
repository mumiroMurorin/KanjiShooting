using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    const StageSceneTag FIRST_STAGESCENE = StageSceneTag.Fighting;

    [SerializeField] Transform playerTransform;
    [SerializeField] WaveManager[] waves;
    [SerializeField] KanjiObjectSpawner kanjiSpawner;
    [SerializeField] SerializeInterface<IQuestionSelector> questionSelector;
    [SerializeField] TimelineManager timelineManager;

    StageSceneTransitionManager sceneTransitionManager;

    private void Start()
    {
        Initialize();
        SetPhaseTransitioner();
        StageStart();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Initialize()
    {
        sceneTransitionManager = new StageSceneTransitionManager();

        //各ウェーブの初期化
        foreach (WaveManager w in waves)
        {
            w.Initialize(playerTransform, kanjiSpawner, questionSelector.Value);
        }

        //スコアの初期化
        ScoreManager.Instance.KillCount = 0;
    }

    /// <summary>
    /// 遷移関数の追加(処理はここに追加)
    /// </summary>
    private void SetPhaseTransitioner()
    {
        List<IStagePhaseTransitioner> stagePhaseTransitioners = new List<IStagePhaseTransitioner>();

        //通常のゲームループ
        //ステージ開始演出
        stagePhaseTransitioners.Add(new StageStartEffectTransition(timelineManager.GetStageStartPlayableDirector()));
        
        //WAVE内ループ
        foreach (WaveManager w in waves)
        {
            //ウェーブ開始演出
            stagePhaseTransitioners.Add(new WaveStartEffectTransition(timelineManager.GetWaveStartPlayableDirector()));
            //ウェーブ開始
            stagePhaseTransitioners.Add(new WaveStartTransition(w));
            //ウェーブ終了処理開始
            stagePhaseTransitioners.Add(new WaveFinishTransition(w));
            //ウェーブ終了演出
            stagePhaseTransitioners.Add(new WaveFinishEffectTransition(timelineManager.GetWaveFinishPlayableDirector()));
        }

        //ステージ終了演出
        stagePhaseTransitioners.Add(new StageFinishEffectTransition(timelineManager.GetStageFinishPlayableDirector()));

        //シーンの追加
        sceneTransitionManager.AddPhaseTransitioner(StageSceneTag.Fighting, GenerateScenePhaseTransitionManager(stagePhaseTransitioners));
        stagePhaseTransitioners = new List<IStagePhaseTransitioner>();

        //ゲームオーバー
        //ゲームオーバー処理開始
        stagePhaseTransitioners.Add(new StageFailedStartTransition());
        //ゲームオーバー演出開始
        stagePhaseTransitioners.Add(new StageFailedStartEffectTransition(timelineManager.GetStageFailedStartPlayableDirector()));
        //ゲームオーバー画面終了演出
        stagePhaseTransitioners.Add(new StageFailedFinishEffectTransition(timelineManager.GetStageFailedFinishPlayableDirector()));
        //ゲームオーバー処理終了
        stagePhaseTransitioners.Add(new StageFailedFinishTransition());

        //シーンの追加
        sceneTransitionManager.AddPhaseTransitioner(StageSceneTag.StageFailed, GenerateScenePhaseTransitionManager(stagePhaseTransitioners));
        stagePhaseTransitioners = new List<IStagePhaseTransitioner>();

    }

    /// <summary>
    /// ScenePhaseTransitionManagerを生成する
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
    /// ステージの開始
    /// </summary>
    /// <param name="token"></param>
    private void StageStart()
    {
        sceneTransitionManager.ExecuteStageScene(FIRST_STAGESCENE);
    }

    public void ChangeStageScene(StageSceneTag tag)
    {
        Debug.Log($"【System】シーン遷移: {tag}");
        sceneTransitionManager.ExecuteStageScene(tag);
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

/// <summary>
/// ステージ内シーンの管理クラス
/// </summary>
public class StageSceneTransitionManager
{
    readonly Dictionary<StageSceneTag, ScenePhaseTransitionManager> stagePhases = new Dictionary<StageSceneTag, ScenePhaseTransitionManager>();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    /// <summary>
    /// ステージ内シーンの追加
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="phaseTransitionManager"></param>
    public void AddPhaseTransitioner(StageSceneTag sceneTag, ScenePhaseTransitionManager phaseTransitionManager)
    {
        if(!stagePhases.TryAdd(sceneTag, phaseTransitionManager))
        {
            Debug.LogWarning($"【System】SceneTag:{sceneTag} に対応するステージ内シーンは既に登録されています");
            return;
        }
    }

    /// <summary>
    /// シーンの遷移、キャンセル処理
    /// </summary>
    /// <param name="sceneTag"></param>
    public void ExecuteStageScene(StageSceneTag sceneTag)
    {
        if(!stagePhases.TryGetValue(sceneTag,out ScenePhaseTransitionManager manager))
        {
            Debug.LogError($"【System】ステージ内シーンが登録されていません: {sceneTag}");
            return;
        }

        //ステージシーンの変更
        //現在実行中のシーケンスを中断、リセット
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
/// ステージシーン内フェーズの遷移を担うクラス
/// </summary>
public class ScenePhaseTransitionManager
{
    readonly List<IStagePhaseTransitioner> transitions = new List<IStagePhaseTransitioner>();

    /// <summary>
    /// 遷移処理の追加
    /// </summary>
    /// <param name="transitioner"></param>
    public void AddTransition(IStagePhaseTransitioner transitioner)
    {
        transitions.Add(transitioner);
    }

    /// <summary>
    /// シーケンスの実行
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async UniTask ExecuteAsync(CancellationToken cancellationToken)
    {
        Debug.Log("【System】ステージ内シーン開始");
        foreach (var transition in transitions)
        {
            await transition.ExecuteAsync(cancellationToken);
        }
        Debug.Log("【System】ステージ内シーン終了");
    }
}