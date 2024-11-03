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
        //�Q�[���I�[�o�[�J�n
        stageFailedStartDirector.Play();

        //�Q�[���I�[�o�[�J�n���o�I���܂ő҂�
        await UniTask.WaitUntil(() => stageFailedStartDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("�ySystem�z�Q�[���I�[�o�[�J�n���o�I��");
    }
}
