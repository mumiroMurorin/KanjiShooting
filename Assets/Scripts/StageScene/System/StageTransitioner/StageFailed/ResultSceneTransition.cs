using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;
using System;

public class ResultSceneTransition : IPhaseTransitioner
{
    const string RESULT_SCENE_NAME = "ResultScene";

    PlayableDirector sceneChangeDirector;
    AsyncOperation changeSceneAcync;

    public ResultSceneTransition(PlayableDirector director)
    {
        sceneChangeDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        // メインスレッドに戻す
        await UniTask.SwitchToMainThread();

        // オペレーションの登録
        changeSceneAcync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(RESULT_SCENE_NAME);
        changeSceneAcync.allowSceneActivation = false;

        try
        {
            // シーンの読み込み
            // なんとシーンのロードはメインスレッド以外では行えない
            LoadResultScene(changeSceneAcync, token).Forget();
        }
        // 例外処理
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        // シーンチェンジ開始
        if (sceneChangeDirector != null)
        {
            sceneChangeDirector.Play();

            // シーンチェンジ演出終了まで待ち
            await UniTask.WaitUntil(() => sceneChangeDirector.state != PlayState.Playing, cancellationToken: token);
        }

        changeSceneAcync.allowSceneActivation = true;

        Debug.Log("【System】シーンチェンジ演出終了");
    }

    /// <summary>
    /// リザルトシーンの読み込み
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTaskVoid LoadResultScene(AsyncOperation sceneChangeOperation, CancellationToken token)
    {
        if (token.IsCancellationRequested) { throw new Exception("シーン読み込みが中断されました"); }

        // 読み込みの開始
        Debug.Log("【System】リザルトシーン読み込み開始");
        await sceneChangeOperation;

        Debug.Log("【System】リザルトシーン読み込み完了");
    }
}
