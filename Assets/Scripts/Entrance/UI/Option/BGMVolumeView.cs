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
        /// BGM�{�����[�����ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="value"></param>
        public void OnBGMVolumeChanged(float value)
        {
            bgmSlider.value = value;
            volumeText.text = (value * 10).ToString("0.00");
        }

        /// <summary>
        /// BGM�X���C�_�[���ς���ꂽ�Ƃ��̃��\�b�h
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
