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

                // ロード演出終了まで待ち
                await UniTask.WaitUntil(() => finishLoadDirector.state != PlayState.Playing, cancellationToken: token);
            }

            // ステージステータスの変更
            EntranceManager.Instance.SetMenuStatus(MenuStatus.StageSelect);

            // タイトル音楽再生
            Sound.SoundManager.Instance.PlayBGM(Sound.BGM_Type.Title);
        }
    }

}
