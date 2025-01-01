using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;
using System;
using UniRx;

namespace EntranceTransition
{
    public class MainSceneTransition : IPhaseTransitioner
    {
        IEntranceUIController entranceUIController;
        IReadOnlyReactiveProperty<StageDetailData> stageTransitionData;
        PlayableDirector sortieDirector;
        AsyncOperation changeSceneAcync;

        public MainSceneTransition(IEntranceUIController uiController, PlayableDirector director, IReadOnlyReactiveProperty<StageDetailData> stageData)
        {
            entranceUIController = uiController;
            sortieDirector = director;
            stageTransitionData = stageData;
        }

        public async UniTask ExecuteAsync(CancellationToken token)
        {
            if (stageTransitionData == null) { return; }

            // BGMの停止
            Sound.SoundManager.Instance.StopBGM(true);

            // メインスレッドに戻す
            await UniTask.SwitchToMainThread();
            // オペレーションの登録
            changeSceneAcync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(stageTransitionData.Value.SceneName);
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
            entranceUIController.ActiveUIGroup(MenuStatus.Sortie);
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

}
