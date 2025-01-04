using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StageUI
{
    public class SpecialActionGuideView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMeshProUGUI;
        [SerializeField] string explosionText = "長押しで自爆！";
        [SerializeField] string shootText = "長押しで爆発攻撃！";

        /// <summary>
        /// チャージ値が変わったときのメソッド
        /// </summary>
        /// <param name="value"></param>
        public void OnChangeChargeValue(float value)
        {
            textMeshProUGUI.enabled = value >= 1f;
        }

        /// <summary>
        /// アクションモード変更
        /// </summary>
        /// <param name="isInputed"></param>
        public void OnChangeActionMode(bool isInputed)
        {
            textMeshProUGUI.text = isInputed ? shootText : explosionText;
        }
    }

}