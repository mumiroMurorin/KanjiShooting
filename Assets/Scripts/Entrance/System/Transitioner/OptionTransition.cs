using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EntranceTransition;
using UnityEngine.Playables;


public class OptionTransition : IPhaseTransitioner
{
    IEntranceUIcontroller entranceUIController;

    public OptionTransition(IEntranceUIcontroller uiController)
    {
        entranceUIController = uiController;
    }

    public async UniTask ExecuteAsync(CancellationToken token)
    {
        // UIÇÃï\é¶èàóù
        entranceUIController.ActiveUIGroup(MenuStatus.Option);
        await UniTask.Delay(0, cancellationToken: token);
    }
}