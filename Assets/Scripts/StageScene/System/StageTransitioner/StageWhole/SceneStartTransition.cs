using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;
using StageTransition;


public class SceneStartTransition : IPhaseTransitioner
{
    PlayableDirector startEffectDirector;

    public SceneStartTransition(PlayableDirector director)
    {
        startEffectDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        //ステージ開始演出
        startEffectDirector.Play();

        //ステージ開始演出終了まで待ち
        await UniTask.WaitUntil(() => startEffectDirector.state != PlayState.Playing, cancellationToken: token);
        StageManager.Instance.ChangeStageStatus(StageStatus.Fighting);

        Debug.Log("【System】シーン開始演出終了");
    }
}
