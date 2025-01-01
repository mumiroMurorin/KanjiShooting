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

                // タイトル演出終了まで待ち
                await UniTask.WaitUntil(() => titleDirector.state != PlayState.Playing, cancellationToken: token);
            }

            // ステージステータスの変更
            EntranceManager.Instance.SetMenuStatus(MenuStatus.MainMenu);

            // タイトル音楽再生
            Sound.SoundManager.Instance.PlayBGM(Sound.BGM_Type.Title);
        }
    }

}
