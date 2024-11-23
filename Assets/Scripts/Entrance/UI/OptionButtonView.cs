using UnityEngine;
using System;

namespace EntranceUI
{
    public class OptionButtonView : MonoBehaviour
    {
        public Action OnOptionButtonClickedListener;

        /// <summary>
        /// 設定ボタンが押されたときのメソッド
        /// </summary>
        public void OnOptionButtonClicked()
        {
            if (OnOptionButtonClickedListener != null)
            {
                OnOptionButtonClickedListener.Invoke();
            }
        }
    }
}