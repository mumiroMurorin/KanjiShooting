using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EntranceTransition;
using UnityEngine.Playables;


public class MainMenuTransition : IPhaseTransitioner
{
    IEntranceUIcontroller entranceUIController;

    public MainMenuTransition(IEntranceUIcontroller uiController)
    {
        entranceUIController = uiController;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        // UIÇÃï\é¶èàóù
        entranceUIController.ActiveUIGroup(MenuStatus.MainMenu);
        await UniTask.Delay(0, cancellationToken: token);
    }
}
