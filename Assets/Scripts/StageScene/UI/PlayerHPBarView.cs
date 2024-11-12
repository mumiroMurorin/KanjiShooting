using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHPBarView : MonoBehaviour
{
    [Header("HPGage親オブジェクト")]
    [SerializeField] Transform parentTransform;

    [Header("HPSlider")]
    [SerializeField] Slider slider;

    [Header("揺れステータス")]
    [SerializeField] ShakeSettings shakeSettings;

    /// <summary>
    /// HPが変わったとき
    /// </summary>
    /// <param name="value"></param>
    public void OnChangeHP(float value)
    {
        // HPが減ったとき震わせる
        if (slider.value > value)
        {
            shakeSettings.ApplyShake(parentTransform);
        }

        slider.value = value;
    }
}
