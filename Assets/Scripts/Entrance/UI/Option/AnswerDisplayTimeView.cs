using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace OptionUI
{
    public class AnswerDisplayTimeView : MonoBehaviour
    {
        [SerializeField] Slider timeSlider;
        [SerializeField] TextMeshProUGUI timerText;
        [SerializeField] Toggle validToggle;

        public Action<float> OnChangeTimeListener;
        public Action<bool> OnChangeValidityListener;

        /// <summary>
        /// �\�����Ԃ��ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="value"></param>
        public void OnTimeValueChanged(float valueNormalized)
        {
            timeSlider.value = valueNormalized;
        }

        /// <summary>
        /// �\�����Ԃ��ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="value"></param>
        public void OnTimeChanged(float time)
        {
            timerText.text = time.ToString("0.0" + "�b");
        }

        /// <summary>
        /// �\�����ԃX���C�_�[���ς���ꂽ�Ƃ��̃��\�b�h
        /// </summary>
        public void OnTimeSliderChanged(float value)
        {
            if (OnChangeTimeListener != null)
            {
                OnChangeTimeListener.Invoke(value);
            }
        }

        /// <summary>
        /// �~���[�g�l���ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="isValid"></param>
        public void OnValidityChanged(bool isValid)
        {
            validToggle.isOn = isValid;
            timeSlider.interactable = isValid;
        }

        /// <summary>
        /// �~���[�g�l���ς���ꂽ�Ƃ��̃��\�b�h
        /// </summary>
        public void OnValidToggleChanged(bool isValid)
        {
            if (OnChangeValidityListener != null)
            {
                OnChangeValidityListener.Invoke(isValid);
            }
        }
    }
}
