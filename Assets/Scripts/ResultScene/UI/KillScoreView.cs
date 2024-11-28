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
            scoreText.text = killNum.ToString() + " ‘Ì";
        }

        public void StartAnimation()
        {
            // –{“–‚ÍDoText‚Å‚È‚ñ‚Å‚àŠÓ’è’c‚İ‚½‚¢‚È‚±‚Æ‚â‚è‚½‚©‚Á‚½‚¯‚Ç
            // DoTweenPro‚µ‚©‘Î‰‚µ‚Ä‚È‚©‚Á‚½c(‚¢‚Â‚©”ƒ‚¤)


        }
    }
}