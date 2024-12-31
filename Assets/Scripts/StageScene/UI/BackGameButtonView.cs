using UnityEngine;
using System;

namespace StageUI
{
    public class BackGameButtonView : MonoBehaviour
    {
        public Action OnBackGameButtonClickedListener;

        /// <summary>
        /// �u�Q�[���ɖ߂�v�{�^���������ꂽ�Ƃ��̃��\�b�h
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