using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;

namespace EntranceTransition
{
    public class FromOtherSceneTransition : IPhaseTransitioner
    {
        PlayableDirector finishLoadDirector;
        IEntranceUIController entranceUIController;

        public FromOtherSceneTransition(IEntranceUIController uiController, PlayableDirector director)
        {
            entranceUIController = uiController;
            finishLoadDirector = director;
        }

        public async UniTask ExecuteAsync(CancellationToken token)
        {
            entranceUIController.ActiveUIGroup(MenuStatus.StageSelect);

            if (finishLoadDirector != null)
            {
                finishLoadDirector.Play();

                // ���[�h���o�I���܂ő҂�
                await UniTask.WaitUntil(() => finishLoadDirector.state != PlayState.Playing, cancellationToken: token);
            }

            // �X�e�[�W�X�e�[�^�X�̕ύX
            EntranceManager.Instance.SetMenuStatus(MenuStatus.StageSelect);

            // �^�C�g�����y�Đ�
            Sound.SoundManager.Instance.PlayBGM(Sound.BGM_Type.Title);
        }
    }

}
