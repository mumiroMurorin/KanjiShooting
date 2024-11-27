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
        /// �X�e�[�W���I�����ꂽ�Ƃ����߂ďo���ł���
        /// </summary>
        public void OnStageSelected(bool isSelected)
        {
            sortieButton.interactable = isSelected;
        }

        /// <summary>
        /// �o���{�^���������ꂽ�Ƃ��̃��\�b�h
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
