using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;

namespace EntranceTransition
{
    public class TitleTransition : IPhaseTransitioner
    {
        PlayableDirector titleDirector;
        IEntranceUIController entranceUIController;

        public TitleTransition(IEntranceUIController uiController, PlayableDirector director)
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

}
