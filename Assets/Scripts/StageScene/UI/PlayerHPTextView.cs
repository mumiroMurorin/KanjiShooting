using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StageUI
{
    public class PlayerHPTextView : MonoBehaviour
    {
        [Header("HPText")]
        [SerializeField] TextMeshProUGUI tmp;
        [Header("�c��̗͂ɉ������F�ω�")]
        [SerializeField] Gradient textGradient;

        /// <summary>
        /// HP���ς�����Ƃ��̃��\�b�h
        /// </summary>
        /// <param name="hp"></param>
        public void OnChangeHP(int hp)
        {
            tmp.text = hp.ToString();
        }

        public void OnChangeHP(float ratio)
        {
            tmp.color = textGradient.Evaluate(ratio);
        }
    }

}
