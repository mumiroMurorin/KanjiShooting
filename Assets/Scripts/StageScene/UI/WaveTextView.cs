using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StageUI
{
    public class WaveTextView : MonoBehaviour
    {
        [Header("WaveText")]
        [SerializeField] TextMeshProUGUI tmp;

        /// <summary>
        /// 現在Waveが変わったときの処理
        /// </summary>
        /// <param name="number"></param>
        public void OnChangeWaveCount(int number)
        {
            if (number > 0) { tmp.text = "WAVE" + number.ToString(); }
            else { tmp.text = ""; }
        }
    }

}
