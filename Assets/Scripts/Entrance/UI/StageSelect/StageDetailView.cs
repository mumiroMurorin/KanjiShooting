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
        [Space(20)]
        [Header("漢字レベルオブジェクト親")]
        [SerializeField] Transform kanjiLevelParent;
        [Header("漢字レベルイメージオブジェクト")]
        [SerializeField] GameObject kanjiLevelObject;
        [Space(20)]
        [Header("タイピングレベルオブジェクト親")]
        [SerializeField] Transform typingLevelParent;
        [Header("タイピングレベルイメージオブジェクト")]
        [SerializeField] GameObject typingLevelObject;

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

            SetKanjiLevel(stageDetailData);
            SetTypingLevel(stageDetailData.TypingLevel);
        }

        /// <summary>
        /// 漢字レベルのセット
        /// </summary>
        private void SetKanjiLevel(StageDetailData stageDetailData)
        {
            // 全削除
            if(kanjiLevelParent.childCount > 0)
            {
                foreach (Transform child in kanjiLevelParent.transform) { Destroy(child.gameObject); }
            }

            // 難易度(★)のインスタンス化
            for (int i = 0; i < stageDetailData.KanjiLevel; i++)
            {
                Instantiate(kanjiLevelObject, kanjiLevelParent);
            }

        }

        /// <summary>
        /// タイピングレベルのセット
        /// </summary>
        private void SetTypingLevel(int level)
        {
            // 全削除
            if (kanjiLevelParent.childCount > 0)
            {
                foreach (Transform child in typingLevelParent.transform) { Destroy(child.gameObject); }
            }

            // 難易度(★)のインスタンス化
            for (int i = 0; i < level; i++)
            {
                Instantiate(typingLevelObject, typingLevelParent);
            }
        }
    }

}
