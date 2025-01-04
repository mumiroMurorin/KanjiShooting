using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StageUI
{
    public class SpecialActionGuideView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMeshProUGUI;
        [SerializeField] string explosionText = "�������Ŏ����I";
        [SerializeField] string shootText = "�������Ŕ����U���I";

        /// <summary>
        /// �`���[�W�l���ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="value"></param>
        public void OnChangeChargeValue(float value)
        {
            textMeshProUGUI.enabled = value >= 1f;
        }

        /// <summary>
        /// �A�N�V�������[�h�ύX
        /// </summary>
        /// <param name="isInputed"></param>
        public void OnChangeActionMode(bool isInputed)
        {
            textMeshProUGUI.text = isInputed ? shootText : explosionText;
        }
    }

}