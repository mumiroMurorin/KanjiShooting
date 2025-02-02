using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

public class WaveFinishTransition : IPhaseTransitioner
{
    WaveManager executeWaveManager;

    public WaveFinishTransition(WaveManager waveManager)
    {
        executeWaveManager = waveManager;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //WAVEI¹®¹ÌR[obNðwÇ
        bool isEndOfWaveFinish = false;
        executeWaveManager?.OnEndWaveFinishingAsObservable
             .Subscribe(_ => { isEndOfWaveFinish = true; })
             .AddTo(token);

        //I¹³¹é
        executeWaveManager?.FinishWave();

        //WAVEI¹®¹ÜÅÒ¿
        await UniTask.WaitUntil(() => isEndOfWaveFinish, PlayerLoopTiming.Update, token);
        Debug.Log("ySystemzWaveI¹");
    }
}