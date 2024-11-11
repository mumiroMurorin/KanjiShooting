using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextWaveView : MonoBehaviour
{
    [Header("NextWaveText")]
    [SerializeField] TextMeshProUGUI tmp;

    public void OnChangePreviousWave(int waveNum)
    {
        tmp.text = "WAVE" + waveNum + " START";
    }
}
