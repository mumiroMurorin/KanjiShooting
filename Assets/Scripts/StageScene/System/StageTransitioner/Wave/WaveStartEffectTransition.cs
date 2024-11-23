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
    ScoreHolder scoreHolder;

    public WaveStartEffectTransition(PlayableDirector director, ScoreHolder holder)
    {
        waveStartDirector = director;
        scoreHolder = holder;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //Waveカウントを更新
        scoreHolder.IncrementWaveCount();

        //ウェーブ開始演出
        waveStartDirector.Play();

        //ウェーブ開始演出終了まで待ち
        await UniTask.WaitUntil(() => waveStartDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("【System】ウェーブ開始演出終了");
    }
}