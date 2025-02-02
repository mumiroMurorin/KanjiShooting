using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class WaveFinishEffectTransition : IPhaseTransitioner
{
    PlayableDirector waveFinishDirector;

    public WaveFinishEffectTransition(PlayableDirector director)
    {
        waveFinishDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //EF[uI¹o
        waveFinishDirector.Play();

        //EF[uI¹oI¹ÜÅÒ¿
        await UniTask.WaitUntil(() => waveFinishDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("ySystemzEF[uI¹oI¹");
    }
}