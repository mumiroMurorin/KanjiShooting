using System;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

namespace StageUI
{
    public class SpecialChargeGaugeView : MonoBehaviour
    {
        [Header("ChargeImage")]
        [SerializeField] Image image;
        [Header("RatioToColor")]
        [SerializeField] Gradient gradient;
        [Header("FillTime")]
        [SerializeField] float fillTime;

        /// <summary>
        /// チャージ値が変わったときのメソッド
        /// </summary>
        /// <param name="value"></param>
        public void OnChangeChargeValue(float value)
        {
            image.gameObject.SetActive(true);
            image.DOFillAmount(value, fillTime);
            image.DOColor(gradient.Evaluate(value), fillTime);
        }
    }

}
