using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace EntranceUI
{
    public class BGMVolumeView : MonoBehaviour
    {
        [SerializeField] Slider bgmSlider;
        [SerializeField] TextMeshProUGUI volumeText;

        public Action<float> OnChangeValueListener;

        /// <summary>
        /// BGMボリュームが変わったときのメソッド
        /// </summary>
        /// <param name="value"></param>
        public void OnBGMVolumeChanged(float value)
        {
            bgmSlider.value = value;
            volumeText.text = (value * 10).ToString("0.00");
        }

        /// <summary>
        /// BGMスライダーが変えられたときのメソッド
        /// </summary>
        public void OnBGMVolumeSliderChanged(float value)
        {
            if (OnChangeValueListener != null)
            {
                OnChangeValueListener.Invoke(value);
            }
        }
    }
}
