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
        //�X�e�[�W�J�n���o
        startEffectDirector.Play();

        //�X�e�[�W�J�n���o�I���܂ő҂�
        await UniTask.WaitUntil(() => startEffectDirector.state != PlayState.Playing, cancellationToken: token);
        StageManager.Instance.ChangeStageStatus(StageStatus.Battling);

        Debug.Log("�ySystem�z�X�e�[�W�J�n���o�I��");
    }
}
