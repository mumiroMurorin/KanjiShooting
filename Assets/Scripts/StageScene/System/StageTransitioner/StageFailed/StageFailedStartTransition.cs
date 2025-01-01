using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using StageTransition;

namespace StageTransition
{
    public class StageFailedStartTransition : IPhaseTransitioner
    {
        ScoreHolder scoreHolder;

        public StageFailedStartTransition(ScoreHolder holder)
        {
            scoreHolder = holder;
        }

        public async UniTask ExecuteAsync(CancellationToken token)
        {
            Debug.Log("【System】GameOver処理開始");

            //スコア記録
            scoreHolder.RecordSurvivalTimeScoreForUnityRoom();
            //BGMのフェードアウト
            Sound.SoundManager.Instance.StopBGM(true);
            StageManager.Instance.ChangeStageStatus(StageStatus.StageFinish);

            await UniTask.Delay(1, cancellationToken: token);
        }
    }

}
