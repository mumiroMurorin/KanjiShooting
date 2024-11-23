using UnityEngine;
using System;

namespace ResultUI
{
    public class BackMainMenuButtonView : MonoBehaviour
    {
        public Action OnBackMainMenuButtonClickedListener;

        /// <summary>
        /// 出撃ボタンが押されたときのメソッド
        /// </summary>
        public void OnBackMainMenuButtonClicked()
        {
            if (OnBackMainMenuButtonClickedListener != null)
            {
                OnBackMainMenuButtonClickedListener.Invoke();
            }
        }
    }
}