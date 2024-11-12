using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EntranceTransition;
using UnityEngine.Playables;


public class MainSceneTransition : IPhaseTransitioner
{
    IEntranceUIcontroller entranceUIcontroller;
    PlayableDirector sortieDirector;

    public MainSceneTransition(IEntranceUIcontroller uiController, PlayableDirector director)
    {
        entranceUIcontroller = uiController;
        sortieDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        sortieDirector?.Play();

        // 出撃演出終了まで待ち
        await UniTask.WaitUntil(() => sortieDirector.state != PlayState.Playing, cancellationToken: token);

        // ステージステータスの変更
        EntranceManager.Instance.SetMenuStatus(MenuStatus.Sortie);
    }
}
