using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace ResultUI
{
    public class KillScoreView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;

        public void OnChangeScore(int killNum)
        {
            scoreText.text = killNum.ToString() + " 体";
        }

        public void StartAnimation()
        {
            // 本当はDoTextでなんでも鑑定団みたいなことやりたかったけど
            // DoTweenProしか対応してなかった…(いつか買う)


        }
    }
}