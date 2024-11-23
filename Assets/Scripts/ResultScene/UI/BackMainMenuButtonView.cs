using UnityEngine;
using System;

namespace ResultUI
{
    public class BackMainMenuButtonView : MonoBehaviour
    {
        public Action OnBackMainMenuButtonClickedListener;

        /// <summary>
        /// �o���{�^���������ꂽ�Ƃ��̃��\�b�h
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