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
            scoreText.text = killNum.ToString() + " ��";
        }

        public void StartAnimation()
        {
            // �{����DoText�łȂ�ł��Ӓ�c�݂����Ȃ��Ƃ�肽����������
            // DoTweenPro�����Ή����ĂȂ������c(��������)


        }
    }
}