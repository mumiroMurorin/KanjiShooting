using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResultTransition;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using VContainer;

public class ResultManager : LocalSingletonMonoBehaviour<ResultManager>
{
    const MenuStatus FIRST_MENU_STATUS = MenuStatus.StartScene;

    [SerializeField] ResultUIController uiController;
    [SerializeField] TimelineManager timelineManager;

    ResultTransitionManager resultTransitionManager = new ResultTransitionManager();
    ReactiveProperty<MenuStatus> currentStatus = new ReactiveProperty<MenuStatus>();
    public IReadOnlyReactiveProperty<MenuStatus> CurentStatusReactiveProperty { get { return currentStatus; } }
    CancellationTokenSource cts;

    ScoreHolder scoreHolder;

    [Inject]
    public void Construct(ScoreHolder sholder)
    {
        scoreHolder = sholder;
    }

    private new void Awake()
    {
        Initialize();
        SetTransitioners();
        Bind();
        SetMenuStatus(FIRST_MENU_STATUS);
    }

    private void Initialize()
    {
        cts = new CancellationTokenSource();
    }

    private void Bind()
    {
        // ステータス変更 → 画面遷移
        CurentStatusReactiveProperty.Subscribe(status =>
        {
            resultTransitionManager.ExecuteAsync(status, cts.Token).Forget();
        })
        .AddTo(this.gameObject);
    }

    /// <summary>
    /// 画面遷移イベントのセット
    /// </summary>
    private void SetTransitioners()
    {
        resultTransitionManager.AddTransition(MenuStatus.StartScene, new StartSceneTransition(uiController, timelineManager.GetPlayableDirector("StartScene")));
        resultTransitionManager.AddTransition(MenuStatus.Result, new StartResultTransition(uiController, timelineManager.GetPlayableDirector("Result")));
        resultTransitionManager.AddTransition(MenuStatus.BackEntrance, new BackEntranceTransition(uiController, scoreHolder, timelineManager.GetPlayableDirector("BackEntrance")));
        resultTransitionManager.AddTransition(MenuStatus.Retry, new RetryStageTransition(uiController, timelineManager.GetPlayableDirector("Retry")));
    }

    /// <summary>
    /// メニューステータスの変更(メニュー画面の遷移)
    /// </summary>
    /// <param name="status"></param>
    public void SetMenuStatus(MenuStatus status)
    {
        currentStatus.Value = status;
    }

    private void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
    }
}
