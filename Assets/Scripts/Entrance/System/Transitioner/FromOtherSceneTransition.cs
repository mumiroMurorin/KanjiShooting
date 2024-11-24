using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EntranceTransition;
using UnityEngine.Playables;


public class FromOtherSceneTransition : IPhaseTransitioner
{
    PlayableDirector finishLoadDirector;
    IEntranceUIcontroller entranceUIController;

    public FromOtherSceneTransition(IEntranceUIcontroller uiController, PlayableDirector director)
    {
        entranceUIController = uiController;
        finishLoadDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        entranceUIController.ActiveUIGroup(MenuStatus.StageSelect);

        if(finishLoadDirector != null)
        {
            finishLoadDirector.Play();

            // ���[�h���o�I���܂ő҂�
            await UniTask.WaitUntil(() => finishLoadDirector.state != PlayState.Playing, cancellationToken: token);
        }

        // �X�e�[�W�X�e�[�^�X�̕ύX
        EntranceManager.Instance.SetMenuStatus(MenuStatus.StageSelect);
    }
}
