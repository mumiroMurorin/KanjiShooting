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

            // カーソルを動かせるようにする
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (resultDirector != null)
            {
                resultDirector.Play();

                // タイトル演出終了まで待ち
                await UniTask.WaitUntil(() => resultDirector.state != PlayState.Playing, cancellationToken: token);
            }
        }
    }

}
