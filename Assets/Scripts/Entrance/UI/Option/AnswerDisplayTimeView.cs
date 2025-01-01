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
        /// 表示時間が変わったときのメソッド
        /// </summary>
        /// <param name="value"></param>
        public void OnTimeValueChanged(float valueNormalized)
        {
            timeSlider.value = valueNormalized;
        }

        /// <summary>
        /// 表示時間が変わったときのメソッド
        /// </summary>
        /// <param name="value"></param>
        public void OnTimeChanged(float time)
        {
            timerText.text = time.ToString("0.0" + "秒");
        }

        /// <summary>
        /// 表示時間スライダーが変えられたときのメソッド
        /// </summary>
        public void OnTimeSliderChanged(float value)
        {
            if (OnChangeTimeListener != null)
            {
                OnChangeTimeListener.Invoke(value);
            }
        }

        /// <summary>
        /// ミュート値が変わったときのメソッド
        /// </summary>
        /// <param name="isValid"></param>
        public void OnValidityChanged(bool isValid)
        {
            validToggle.isOn = isValid;
            timeSlider.interactable = isValid;
        }

        /// <summary>
        /// ミュート値が変えられたときのメソッド
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
