using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using StageTransition;


public class StageFailedStartTransition : IPhaseTransitioner
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
