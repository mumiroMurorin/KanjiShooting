using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;

public class StageStartEffectTransition : IStagePhaseTransitioner
{
    PlayableDirector startEffectDirector;

    public StageStartEffectTransition(PlayableDirector director)
    {
        startEffectDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //ステージ開始演出
        startEffectDirector.Play();

        //ステージ開始演出終了まで待ち
        await UniTask.WaitUntil(() => startEffectDirector.state != PlayState.Playing, cancellationToken: token);
        StageManager.Instance.ChangeStageStatus(StageStatus.Battling);

        Debug.Log("【System】ステージ開始演出終了");
    }
}
