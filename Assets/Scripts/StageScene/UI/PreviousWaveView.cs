using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StageUI
{
    public class PreviousWaveView : MonoBehaviour
    {
        [Header("PreviousWaveText")]
        [SerializeField] TextMeshProUGUI tmp;

        public void OnChangePreviousWave(int waveNum)
        {
            tmp.text = "WAVE" + waveNum + " FINISH";
        }
    }

}