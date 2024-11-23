using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace EntranceUI
{
    public class MouseSensitivityView : MonoBehaviour
    {
        [SerializeField] Slider sensitivitySlider;
        [SerializeField] TextMeshProUGUI sensitivityText;
        [SerializeField] Toggle validToggle;

        public Action<float> OnChangeSensitivityListener;
        public Action<bool> OnChangeValidValueListener;

        /// <summary>
        /// �}�E�X���x���ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="value"></param>
        public void OnSensitivityChanged(float value)
        {
            sensitivitySlider.value = value;
            sensitivityText.text = (value * 10).ToString("0.00");
        }

        /// <summary>
        /// �}�E�X���x�X���C�_�[���ς���ꂽ�Ƃ��̃��\�b�h
        /// </summary>
        public void OnSensiticvitySliderChanged(float value)
        {
            if (OnChangeSensitivityListener != null)
            {
                OnChangeSensitivityListener.Invoke(value);
            }
        }

        /// <summary>
        /// �~���[�g�l���ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="isValid"></param>
        public void OnValidityChanged(bool isValid)
        {
            validToggle.isOn = isValid;
            sensitivitySlider.interactable = isValid;
        }

        /// <summary>
        /// �~���[�g�l���ς���ꂽ�Ƃ��̃��\�b�h
        /// </summary>
        public void OnValidToggleChanged(bool isValid)
        {
            if (OnChangeValidValueListener != null)
            {
                OnChangeValidValueListener.Invoke(isValid);
            }
        }
    }
}
