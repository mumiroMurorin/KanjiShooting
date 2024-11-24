using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EntranceTransition;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using VContainer;

public class EntranceManager : LocalSingletonMonoBehaviour<EntranceManager>
{
    [SerializeField] EntranceUIController uiController;
    [SerializeField] TimelineManager timelineManager;

    MenuTransitionManager menuTransitionManager = new MenuTransitionManager();
    ReactiveProperty<MenuStatus> currentStatus = new ReactiveProperty<MenuStatus>();
    public IReadOnlyReactiveProperty<MenuStatus> CurentStatusReactiveProperty { get { return currentStatus; } }
    CancellationTokenSource cts;

    ScoreHolder scoreHolder;

    [Inject]
    public void Construct(ScoreHolder sholder)
    {
        scoreHolder = sholder;
        SetMenuStatus(scoreHolder.InitialEntranceMenuStatus);
    }

    private new void Awake()
    {
        Initialize();
        SetTransitioners();
        Bind();
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
                menuTransitionManager.ExecuteAsync(status, cts.Token).Forget();
            })
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// 画面遷移イベントのセット
    /// </summary>
    private void SetTransitioners()
    {
        menuTransitionManager.AddTransition(MenuStatus.Title, new TitleTransition(uiController, timelineManager.GetPlayableDirector("Title")));
        menuTransitionManager.AddTransition(MenuStatus.MainMenu, new MainMenuTransition(uiController));
        menuTransitionManager.AddTransition(MenuStatus.StageSelectFromOtherScene, new FromOtherSceneTransition(uiController, timelineManager.GetPlayableDirector("LoadFinish")));
        menuTransitionManager.AddTransition(MenuStatus.Option, new OptionTransition(uiController));
        menuTransitionManager.AddTransition(MenuStatus.StageSelect, new StageSelectTransition(uiController));
        menuTransitionManager.AddTransition(MenuStatus.Sortie, new MainSceneTransition(uiController, timelineManager.GetPlayableDirector("Sortie")));
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
