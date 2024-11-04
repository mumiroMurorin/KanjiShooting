using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;

public class StageFinishEffectTransition : IStagePhaseTransitioner
{
    PlayableDirector finishEffectDirector;

    public StageFinishEffectTransition(PlayableDirector director)
    {
        finishEffectDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //ステージ終了演出
        finishEffectDirector.Play();

        //ステージ開始演出終了まで待ち
        await UniTask.WaitUntil(() => finishEffectDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("【System】ステージ終了演出終了");
    }
}
