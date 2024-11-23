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
        /// SE�{�����[�����ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="value"></param>
        public void OnSEVolumeChanged(float value)
        {
            seSlider.value = value;
            volumeText.text = (value * 10).ToString("0.00");
        }

        /// <summary>
        /// SE�X���C�_�[���ς���ꂽ�Ƃ��̃��\�b�h
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
