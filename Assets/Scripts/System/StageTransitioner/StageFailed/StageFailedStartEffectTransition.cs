using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class StageFailedStartEffectTransition : IStagePhaseTransitioner
{
    PlayableDirector stageFailedStartDirector;

    public StageFailedStartEffectTransition(PlayableDirector director)
    {
        stageFailedStartDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //ゲームオーバー開始
        stageFailedStartDirector.Play();

        //ゲームオーバー開始演出終了まで待ち
        await UniTask.WaitUntil(() => stageFailedStartDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("【System】ゲームオーバー開始演出終了");
    }
}
