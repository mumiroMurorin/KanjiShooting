using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletReloadGageView : MonoBehaviour
{
    [Header("ChargeSlider")]
    [SerializeField] Slider slider;

    //リロード値が変わったときのメソッド
    public void OnChangeReloadValue(float value)
    {
        slider.value = value;
    }
}
