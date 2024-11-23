using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using ResultTransition;
using UnityEngine.Playables;
using System;

public class RetryStageTransition : IPhaseTransitioner
{
    const string ENTRANCE_SCENE_NAME = "MainScene";

    IResultUIcontroller resultUIcontroller;
    PlayableDirector endSceneDirector;
    AsyncOperation changeSceneAcync;

    public RetryStageTransition(IResultUIcontroller uiController, PlayableDirector director)
    {
        resultUIcontroller = uiController;
        endSceneDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        // メインスレッドに戻す
        await UniTask.SwitchToMainThread();
        // オペレーションの登録
        changeSceneAcync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ENTRANCE_SCENE_NAME);
        changeSceneAcync.allowSceneActivation = false;

        try
        {
            // シーンの読み込み
            // なんとシーンのロードはメインスレッド以外では行えない
            LoadMainScene(changeSceneAcync, token).Forget();
        }
        // 例外処理
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        // アニメーションの再生
        resultUIcontroller.ActiveUIGroup(MenuStatus.Retry);
        if (endSceneDirector != null)
        {
            endSceneDirector.Play();

            // 出撃演出終了まで待ち
            await UniTask.WaitUntil(() => endSceneDirector.state != PlayState.Playing, cancellationToken: token);
        }

        changeSceneAcync.allowSceneActivation = true;
    }

    /// <summary>
    /// メインシーンの読み込み
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTaskVoid LoadMainScene(AsyncOperation sceneChangeOperation, CancellationToken token)
    {
        if (token.IsCancellationRequested) { throw new Exception("シーン読み込みが中断されました"); }

        // 読み込みの開始
        Debug.Log("【System】メインシーン読み込み開始");
        await sceneChangeOperation;

        Debug.Log("【System】メインシーン読み込み完了");
    }
}
