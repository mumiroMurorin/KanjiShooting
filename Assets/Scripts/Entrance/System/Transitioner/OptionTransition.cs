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
        // UIの表示処理
        entranceUIController.ActiveUIGroup(MenuStatus.MainMenu);
        await UniTask.Delay(0, cancellationToken: token);
    }
}