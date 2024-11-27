using UnityEngine;
using System;
using UnityEngine.UI;

namespace EntranceUI
{
    public class DecisionStageButtonView : MonoBehaviour
    {
        public Button sortieButton;
        public Action OnDecisionStageButtonClickedListener;

        /// <summary>
        /// ステージが選択されたとき初めて出撃できる
        /// </summary>
        public void OnStageSelected(bool isSelected)
        {
            sortieButton.interactable = isSelected;
        }

        /// <summary>
        /// 出撃ボタンが押されたときのメソッド
        /// </summary>
        public void OnDecisionStageButtonClicked()
        {
            if (OnDecisionStageButtonClickedListener != null)
            {
                OnDecisionStageButtonClickedListener.Invoke();
            }
        }
    }
}
