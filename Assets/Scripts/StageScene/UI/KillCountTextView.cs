using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StageUI
{
    public class KillCountTextView : MonoBehaviour
    {
        [Header("killCountText")]
        [SerializeField] TextMeshProUGUI tmp;

        //�L�������ς�����Ƃ��̃��\�b�h
        public void OnChangeKillCount(int count)
        {
            tmp.text = count.ToString();
        }
    }

}
