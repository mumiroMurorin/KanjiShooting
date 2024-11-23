using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace EntranceTransition
{
    public enum MenuStatus
    {
        //GameDataLoad,
        Title,
        MainMenu,
        StageSelect,
        Option,
        Sortie
    }

    /// <summary>
    /// ステージシーン内フェーズの遷移を担うクラス
    /// </summary>
    public class MenuTransitionManager
    {
        Dictionary<MenuStatus, IPhaseTransitioner> transitions = new Dictionary<MenuStatus, IPhaseTransitioner>();

        /// <summary>
        /// 遷移処理の追加
        /// </summary>
        /// <param name="transitioner"></param>
        public void AddTransition(MenuStatus menuStatus, IPhaseTransitioner transitioner)
        {
            transitions.Add(menuStatus, transitioner);
        }

        /// <summary>
        /// シーケンスの実行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async UniTask ExecuteAsync(MenuStatus menuStatus, CancellationToken cancellationToken)
        {
            if(!transitions.TryGetValue(menuStatus,out IPhaseTransitioner phaseTransitioner))
            {
                Debug.LogError($"【System】ステータス {menuStatus}は登録されていません");
                return;
            }

            Debug.Log($"【System】メニュー移動開始: {menuStatus}");
            await phaseTransitioner.ExecuteAsync(cancellationToken);
            Debug.Log($"【System】メニュー移動終了: {menuStatus}");
        }
    }

    public interface IEntranceUIcontroller
    {
        public void ActiveUIGroup(MenuStatus status);
    }
}