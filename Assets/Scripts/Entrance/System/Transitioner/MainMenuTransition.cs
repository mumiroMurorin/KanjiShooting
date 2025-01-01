using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using EntranceTransition;
using UnityEngine.Playables;

namespace EntranceTransition
{
    public class MainMenuTransition : IPhaseTransitioner
    {
        IEntranceUIController entranceUIController;

        public MainMenuTransition(IEntranceUIController uiController)
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

}
