using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;
using System;

namespace StageTransition
{
    public class BackEntranceTransition : IPhaseTransitioner
    {
        const string RESULT_SCENE_NAME = "Entrance";

        PlayableDirector sceneChangeDirector;
        AsyncOperation changeSceneAcync;
        ScoreHolder scoreHolder;

        public BackEntranceTransition(PlayableDirector director, ScoreHolder holder)
        {
            sceneChangeDirector = director;
            scoreHolder = holder;
        }

        public async UniTask ExecuteAsync(CancellationToken token)
        {
            // 時を進める
            Time.timeScale = 1;

            // エントランスはステージセレクトから
            scoreHolder.InitialEntranceMenuStatus = EntranceTransition.MenuStatus.StageSelectFromOtherScene;

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
            Debug.Log("【System】エントランスシーン読み込み開始");
            await sceneChangeOperation;

            Debug.Log("【System】エントランスシーン読み込み完了");
        }
    }

}
