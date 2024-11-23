using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EntranceUI
{
    public class StageDetailView : MonoBehaviour
    {
        [Header("説明文")]
        [SerializeField] TextMeshProUGUI stageDescriptionText;
        [Header("出現漢字例")]
        [SerializeField] TextMeshProUGUI kanjiExampleText;
        [Header("漢字レベル")]
        [SerializeField] TextMeshProUGUI kanjiLevelText;
        [Header("タイピングレベル")]
        [SerializeField] TextMeshProUGUI typingLevelText;

        StageDetailData currentData;

        /// <summary>
        /// ステージが選択されたときのメソッド
        /// </summary>
        public void OnSelectStage(StageDetailData stageDetailData)
        {
            // 選択されたのが同じデータだったら変えない
            if (currentData == stageDetailData) { return; }

            currentData = stageDetailData;

            // データの更新
            stageDescriptionText.text = stageDetailData.StageDescription;
            kanjiExampleText.text = stageDetailData.KanjiExample;
            kanjiLevelText.text = stageDetailData.KanjiLevel.ToString();
            typingLevelText.text = stageDetailData.TypingLevel.ToString();
        }

    }

}
