using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

public class StageFailedStartTransition : IStagePhaseTransitioner
{
    public StageFailedStartTransition()
    {
       
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        Debug.Log("【System】GameOver処理開始");
        
        //スコア記録
        ScoreManager.Instance.RecordSurvivalTimeScoreForUnityRoom();
        //BGMのフェードアウト
        Sound.SoundManager.Instance.StopBGM(true);
        StageManager.Instance.ChangeStageStatus(StageStatus.StageFinish);

        await UniTask.Delay(1, cancellationToken: token);
    }
}
