using UnityEngine;
using System;

namespace StageUI
{
    public class BackGameButtonView : MonoBehaviour
    {
        public Action OnBackGameButtonClickedListener;

        /// <summary>
        /// 「ゲームに戻る」ボタンが押されたときのメソッド
        /// </summary>
        public void OnBackMainMenuButtonClicked()
        {
            if (OnBackGameButtonClickedListener != null)
            {
                OnBackGameButtonClickedListener.Invoke();
            }
        }
    }
}