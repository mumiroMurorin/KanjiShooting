using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using StageTransition;


public class StageFinishTransition : IPhaseTransitioner
{
    ScoreHolder scoreHolder;

    public StageFinishTransition(ScoreHolder holder)
    {
        scoreHolder = holder;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        // レコードの記録
        scoreHolder.RecordSurvivalTimeScoreForUnityRoom();

        // ステージステータスの変更
        StageManager.Instance.ChangeStageStatus(StageStatus.StageFinish);

        // ステージ終了まで待ち
        await UniTask.WaitForSeconds(0.1f, cancellationToken: token);
        Debug.Log("【System】ステージ終了");
    }
}
