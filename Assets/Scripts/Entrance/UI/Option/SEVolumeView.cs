using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace EntranceUI
{
    public class SEVolumeView : MonoBehaviour
    {
        [SerializeField] Slider seSlider;
        [SerializeField] TextMeshProUGUI volumeText;

        public Action<float> OnChangeValueListener;

        /// <summary>
        /// SEボリュームが変わったときのメソッド
        /// </summary>
        /// <param name="value"></param>
        public void OnSEVolumeChanged(float value)
        {
            seSlider.value = value;
            volumeText.text = (value * 10).ToString("0.00");
        }

        /// <summary>
        /// SEスライダーが変えられたときのメソッド
        /// </summary>
        public void OnSEVolumeSliderChanged(float value)
        {
            if (OnChangeValueListener != null)
            {
                OnChangeValueListener.Invoke(value);
            }
        }
    }
}
