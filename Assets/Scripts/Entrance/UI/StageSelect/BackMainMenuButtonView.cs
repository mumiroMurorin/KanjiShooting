using UnityEngine;
using System;

namespace EntranceUI
{
    public class BackMainMenuButtonView : MonoBehaviour
    {
        public Action OnBackMainMenuButtonClickedListener;

        /// <summary>
        /// �u���C�����j���[�ɖ߂�v�{�^���������ꂽ�Ƃ��̃��\�b�h
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