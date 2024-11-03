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
        //ゲームオーバー演出
        stageFailedFinishDirector.Play();

        //ゲームオーバー演出終了まで待ち
        await UniTask.WaitUntil(() => stageFailedFinishDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("【System】ゲームオーバー終了演出終了");
    }
}
