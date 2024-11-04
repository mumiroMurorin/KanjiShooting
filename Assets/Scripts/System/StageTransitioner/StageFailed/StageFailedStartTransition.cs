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
        Debug.Log("�ySystem�zGameOver�����J�n");
        ScoreManager.Instance.RecordSurvivalTimeScoreForUnityRoom();

        await UniTask.Delay(1, cancellationToken: token);
    }
}
