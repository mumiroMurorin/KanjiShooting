using UnityEngine;
using System;

namespace ResultUI
{
    public class RetryButtonView : MonoBehaviour
    {
        public Action OnRetryButtonClickedListener;

        /// <summary>
        /// リトライボタンが押されたときのメソッド
        /// </summary>
        public void OnRetryButtonClicked()
        {
            if (OnRetryButtonClickedListener != null)
            {
                OnRetryButtonClickedListener.Invoke();
            }
        }
    }
}