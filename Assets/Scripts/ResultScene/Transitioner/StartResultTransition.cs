using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;

namespace ResultTransition
{
    public class StartResultTransition : IPhaseTransitioner
    {
        IResultUIcontroller resultUIcontroller;
        PlayableDirector resultDirector;

        public StartResultTransition(IResultUIcontroller uiController, PlayableDirector director)
        {
            resultUIcontroller = uiController;
            resultDirector = director;
        }

        public async UniTask ExecuteAsync(CancellationToken token)
        {
            resultUIcontroller.ActiveUIGroup(MenuStatus.Result);

            // �J�[�\���𓮂�����悤�ɂ���
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (resultDirector != null)
            {
                resultDirector.Play();

                // �^�C�g�����o�I���܂ő҂�
                await UniTask.WaitUntil(() => resultDirector.state != PlayState.Playing, cancellationToken: token);
            }
        }
    }

}
