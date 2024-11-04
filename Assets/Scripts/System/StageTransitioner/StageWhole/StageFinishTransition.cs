using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;

public class StageFinishTransition : IStagePhaseTransitioner
{
    public StageFinishTransition()
    {
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        ScoreManager.Instance.RecordSurvivalTimeScoreForUnityRoom();

        StageManager.Instance.ChangeStageStatus(StageStatus.StageFinish);

        //ステージ終了まで待ち
        await UniTask.WaitForSeconds(0.1f, cancellationToken: token);
        Debug.Log("【System】ステージ終了");
    }
}
