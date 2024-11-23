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
        [Header("残り体力に応じた色変化")]
        [SerializeField] Gradient textGradient;

        /// <summary>
        /// HPが変わったときのメソッド
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
