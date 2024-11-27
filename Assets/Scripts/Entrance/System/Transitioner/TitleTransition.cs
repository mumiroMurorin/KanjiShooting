using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EntranceTransition;
using UnityEngine.Playables;


public class TitleTransition : IPhaseTransitioner
{
    PlayableDirector titleDirector;
    IEntranceUIcontroller entranceUIController;

    public TitleTransition(IEntranceUIcontroller uiController, PlayableDirector director)
    {
        entranceUIController = uiController; 
        titleDirector = director;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        entranceUIController.ActiveUIGroup(MenuStatus.Title);

        if (titleDirector != null)
        {
            titleDirector.Play();

            // �^�C�g�����o�I���܂ő҂�
            await UniTask.WaitUntil(() => titleDirector.state != PlayState.Playing, cancellationToken: token);
        }

        // �X�e�[�W�X�e�[�^�X�̕ύX
        EntranceManager.Instance.SetMenuStatus(MenuStatus.MainMenu);

        // �^�C�g�����y�Đ�
        Sound.SoundManager.Instance.PlayBGM(Sound.BGM_Type.Title);
    }
}
