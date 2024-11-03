using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class StageFailedFinishEffectTransition : IStagePhaseTransitioner
{
    PlayableDirector stageFailedFinishDirector;

    public StageFailedFinishEffectTransition(PlayableDirector director)
    {
        stageFailedFinishDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //Q[I[o[o
        stageFailedFinishDirector.Play();

        //Q[I[o[oI¹ÜÅÒ¿
        await UniTask.WaitUntil(() => stageFailedFinishDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("ySystemzQ[I[o[I¹oI¹");
    }
}
