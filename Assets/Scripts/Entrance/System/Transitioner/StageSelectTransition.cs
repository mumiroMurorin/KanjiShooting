using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Playables;


namespace EntranceTransition
{
    public class StageSelectTransition : IPhaseTransitioner
    {
        IEntranceUIController entranceUIController;

        public StageSelectTransition(IEntranceUIController uiController)
        {
            entranceUIController = uiController;
        }

        public async UniTask ExecuteAsync(CancellationToken token)
        {
            // UIÇÃï\é¶èàóù
            entranceUIController.ActiveUIGroup(MenuStatus.StageSelect);
            await UniTask.Delay(0, cancellationToken: token);
        }
    }

}
