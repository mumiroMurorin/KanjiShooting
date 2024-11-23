using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EntranceUI
{
    public class StageImageBackView : MonoBehaviour
    {
        [Header("背景")]
        [SerializeField] Image backGroundImage;

        /// <summary>
        /// ステージ選択時
        /// </summary>
        public void OnSelectStage(Sprite sprite)
        {
            if (backGroundImage.sprite == sprite) { return; }
            backGroundImage.sprite = sprite;
        }
    }

}