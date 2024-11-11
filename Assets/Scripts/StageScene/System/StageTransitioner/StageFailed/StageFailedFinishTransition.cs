using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

public class StageFailedFinishTransition : IPhaseTransitioner
{
    public StageFailedFinishTransition()
    {

    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        Debug.Log("ÅySystemÅzGameOverââèoäJén");

        await UniTask.Delay(1, cancellationToken: token);
    }
}
