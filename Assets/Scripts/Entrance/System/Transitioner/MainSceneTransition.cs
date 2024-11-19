using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EntranceTransition;
using UnityEngine.Playables;
using System;


public class MainSceneTransition : IPhaseTransitioner
{
    const string MAIN_SCENE_NAME = "MainScene";

    IEntranceUIcontroller entranceUIcontroller;
    PlayableDirector sortieDirector;
    AsyncOperation changeSceneAcync;

    public MainSceneTransition(IEntranceUIcontroller uiController, PlayableDirector director)
    {
        entranceUIcontroller = uiController;
        sortieDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        // メインスレッドに戻す
        await UniTask.SwitchToMainThread();
        // オペレーションの登録
        changeSceneAcync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(MAIN_SCENE_NAME);
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
        entranceUIcontroller.ActiveUIGroup(MenuStatus.Sortie);
        if (sortieDirector != null)
        {
            sortieDirector.Play();

            // 出撃演出終了まで待ち
            await UniTask.WaitUntil(() => sortieDirector.state != PlayState.Playing, cancellationToken: token);
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
