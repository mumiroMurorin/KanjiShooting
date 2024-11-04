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
        //�X�e�[�W�I�����o
        finishEffectDirector.Play();

        //�X�e�[�W�J�n���o�I���܂ő҂�
        await UniTask.WaitUntil(() => finishEffectDirector.state != PlayState.Playing, cancellationToken: token);
        Debug.Log("�ySystem�z�X�e�[�W�I�����o�I��");
    }
}
