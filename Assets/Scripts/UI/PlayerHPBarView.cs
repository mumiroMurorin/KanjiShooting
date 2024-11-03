using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;

public class PlayerHPBarView : MonoBehaviour
{
    [Header("HPSlider")]
    [SerializeField] Slider slider;

    //�������ς�����Ƃ��̃��\�b�h
    public void OnChangeHP(float value)
    {
        slider.value = value;
    }
}
