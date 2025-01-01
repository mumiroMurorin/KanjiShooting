using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;

namespace ResultTransition
{
    public class StartSceneTransition : IPhaseTransitioner
    {
        IResultUIcontroller resultUIcontroller;
        PlayableDirector startSceneDirector;

        public StartSceneTransition(IResultUIcontroller uiController, PlayableDirector director)
        {
            resultUIcontroller = uiController;
            startSceneDirector = director;
        }

        public async UniTask ExecuteAsync(CancellationToken token)
        {
            resultUIcontroller.ActiveUIGroup(MenuStatus.StartScene);

            if (startSceneDirector != null)
            {
                startSceneDirector.Play();

                // �^�C�g�����o�I���܂ő҂�
                await UniTask.WaitUntil(() => startSceneDirector.state != PlayState.Playing, cancellationToken: token);
            }

            // BGM�Đ�
            Sound.SoundManager.Instance.PlayBGM(Sound.BGM_Type.Result);

            // �X�e�[�W�X�e�[�^�X�̕ύX
            ResultManager.Instance.SetMenuStatus(MenuStatus.Result);
        }
    }

}
