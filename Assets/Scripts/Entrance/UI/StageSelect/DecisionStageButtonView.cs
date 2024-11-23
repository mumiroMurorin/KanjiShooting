using UnityEngine;
using System;

namespace EntranceUI
{
    public class DecisionStageButtonView : MonoBehaviour
    {
        public Action OnDecisionStageButtonClickedListener;

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
