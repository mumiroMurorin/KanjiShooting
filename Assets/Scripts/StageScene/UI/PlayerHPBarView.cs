using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHPBarView : MonoBehaviour
{
    [Header("HPGage�e�I�u�W�F�N�g")]
    [SerializeField] Transform parentTransform;

    [Header("HPSlider")]
    [SerializeField] Slider slider;

    [Header("�h��X�e�[�^�X")]
    [SerializeField] ShakeSettings shakeSettings;

    /// <summary>
    /// HP���ς�����Ƃ�
    /// </summary>
    /// <param name="value"></param>
    public void OnChangeHP(float value)
    {
        // HP���������Ƃ��k�킹��
        if (slider.value > value)
        {
            shakeSettings.ApplyShake(parentTransform);
        }

        slider.value = value;
    }
}
