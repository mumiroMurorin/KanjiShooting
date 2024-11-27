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

            // タイトル演出終了まで待ち
            await UniTask.WaitUntil(() => titleDirector.state != PlayState.Playing, cancellationToken: token);
        }

        // ステージステータスの変更
        EntranceManager.Instance.SetMenuStatus(MenuStatus.MainMenu);

        // タイトル音楽再生
        Sound.SoundManager.Instance.PlayBGM(Sound.BGM_Type.Title);
    }
}
