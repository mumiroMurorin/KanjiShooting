using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;

namespace EntranceTransition
{
    public class OptionTransition : IPhaseTransitioner
    {
        IEntranceUIController entranceUIController;

        public OptionTransition(IEntranceUIController uiController)
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
}
