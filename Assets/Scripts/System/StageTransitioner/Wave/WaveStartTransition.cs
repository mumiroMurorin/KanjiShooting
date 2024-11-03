using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;

public class WaveStartTransition : IStagePhaseTransitioner
{
    WaveManager executeWaveManager;

    public WaveStartTransition(WaveManager waveManager)
    {
        executeWaveManager = waveManager;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        Debug.Log("【System】Wave開始");
       
        //WAVE終了時のコールバックを購読
        bool isEndOfWave = false;
        executeWaveManager?.OnEndWaveAsObservable
             .Subscribe(_ => { isEndOfWave = true; })
             .AddTo(token);

        //スタートさせる
        executeWaveManager?.StartWave();

        //ウェーブ終了まで待ち
        await UniTask.WaitUntil(() => isEndOfWave, PlayerLoopTiming.Update, token);
    }
}