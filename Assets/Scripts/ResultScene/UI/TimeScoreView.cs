using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ResultUI
{
    public class TimeScoreView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;

        public void OnChangeScore(float time)
        {
            scoreText.text = FormatTime(time);
        }


        /// <summary>
        /// floatを「hh:mm:ss」にコンバート
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        string FormatTime(float seconds)
        {
            int totalSeconds = Mathf.FloorToInt(seconds); // 小数点以下を切り捨て
            int hours = totalSeconds / 3600;              // 時間
            int minutes = (totalSeconds % 3600) / 60;     // 分
            int secs = totalSeconds % 60;                 // 秒

            // "00:00:00" フォーマットで返す
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, secs);
        }
    }
}