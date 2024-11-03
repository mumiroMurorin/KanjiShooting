using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

public class WaveFinishTransition : IStagePhaseTransitioner
{
    WaveManager executeWaveManager;

    public WaveFinishTransition(WaveManager waveManager)
    {
        executeWaveManager = waveManager;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //WAVE終了処理完了時のコールバックを購読
        bool isEndOfWaveFinish = false;
        executeWaveManager?.OnEndWaveFinishingAsObservable
             .Subscribe(_ => { isEndOfWaveFinish = true; })
             .AddTo(token);

        //終了させる
        executeWaveManager?.FinishWave();

        //WAVE終了処理完了まで待ち
        await UniTask.WaitUntil(() => isEndOfWaveFinish, PlayerLoopTiming.Update, token);
        Debug.Log("【System】Wave終了");
    }
}