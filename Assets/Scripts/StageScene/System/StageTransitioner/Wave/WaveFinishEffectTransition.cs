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
        //ウェーブ終了演出
        waveFinishDirector.Play();

        //ウェーブ終了演出終了まで待ち
        await UniTask.WaitUntil(() => waveFinishDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("【System】ウェーブ終了演出終了");
    }
}