using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class WaveStartEffectTransition : IPhaseTransitioner
{
    PlayableDirector waveStartDirector;

    public WaveStartEffectTransition(PlayableDirector director)
    {
        waveStartDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //Waveカウントを更新
        ScoreManager.Instance.IncrementWaveCount();

        //ウェーブ開始演出
        waveStartDirector.Play();

        //ウェーブ開始演出終了まで待ち
        await UniTask.WaitUntil(() => waveStartDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("【System】ウェーブ開始演出終了");
    }
}