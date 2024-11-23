using UnityEngine;
using System;

namespace ResultUI
{
    public class RetryButtonView : MonoBehaviour
    {
        public Action OnRetryButtonClickedListener;

        /// <summary>
        /// ���g���C�{�^���������ꂽ�Ƃ��̃��\�b�h
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