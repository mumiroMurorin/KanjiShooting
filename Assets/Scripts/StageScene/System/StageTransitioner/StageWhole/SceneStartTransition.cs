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
        //�X�e�[�W�J�n���o
        startEffectDirector.Play();

        //�X�e�[�W�J�n���o�I���܂ő҂�
        await UniTask.WaitUntil(() => startEffectDirector.state != PlayState.Playing, cancellationToken: token);
        StageManager.Instance.ChangeStageStatus(StageStatus.Fighting);

        Debug.Log("�ySystem�z�V�[���J�n���o�I��");
    }
}
