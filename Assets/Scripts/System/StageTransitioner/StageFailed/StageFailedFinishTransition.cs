using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

public class StageFailedFinishTransition : IStagePhaseTransitioner
{
    public StageFailedFinishTransition()
    {

    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        Debug.Log("�ySystem�zGameOver���o�J�n");

        await UniTask.Delay(1, cancellationToken: token);
    }
}
