using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHPTextView : MonoBehaviour
{
    [Header("HPText")]
    [SerializeField] TextMeshProUGUI tmp;

    //HP���ς�����Ƃ��̃��\�b�h
    public void OnChangeHP(int hp)
    {
        tmp.text = hp.ToString();
    }
}
