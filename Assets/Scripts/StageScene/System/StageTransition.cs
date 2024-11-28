using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace StageTransition
{
    public enum StageSceneTag
    {
        Fighting,
        StageFailed
    }

    public enum StageStatus
    {
        Loading,
        Fighting,
        StageFinish,
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
            if (!stagePhases.TryAdd(sceneTag, phaseTransitionManager))
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
            if (!stagePhases.TryGetValue(sceneTag, out ScenePhaseTransitionManager manager))
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
        readonly List<IPhaseTransitioner> transitions = new List<IPhaseTransitioner>();

        /// <summary>
        /// 遷移処理の追加
        /// </summary>
        /// <param name="transitioner"></param>
        public void AddTransition(IPhaseTransitioner transitioner)
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

    public interface IStageUIcontroller
    {
        public void ActiveUIGroup(StageStatus status);
    }
}