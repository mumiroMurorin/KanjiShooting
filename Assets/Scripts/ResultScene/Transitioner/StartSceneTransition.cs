using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using ResultTransition;
using UnityEngine.Playables;

public class StartSceneTransition : IPhaseTransitioner
{
    IResultUIcontroller resultUIcontroller;
    PlayableDirector startSceneDirector;

    public StartSceneTransition(IResultUIcontroller uiController, PlayableDirector director)
    {
        resultUIcontroller = uiController;
        startSceneDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        resultUIcontroller.ActiveUIGroup(MenuStatus.StartScene);

        if (startSceneDirector != null)
        {
            startSceneDirector.Play();

            // タイトル演出終了まで待ち
            await UniTask.WaitUntil(() => startSceneDirector.state != PlayState.Playing, cancellationToken: token);
        }

        // ステージステータスの変更
        ResultManager.Instance.SetMenuStatus(MenuStatus.Result);
    }
}
