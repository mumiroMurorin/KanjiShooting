using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EntranceUI
{
    public class StageDetailView : MonoBehaviour
    {
        [Header("������")]
        [SerializeField] TextMeshProUGUI stageDescriptionText;
        [Header("�o��������")]
        [SerializeField] TextMeshProUGUI kanjiExampleText;
        [Header("�������x��")]
        [SerializeField] TextMeshProUGUI kanjiLevelText;
        [Header("�^�C�s���O���x��")]
        [SerializeField] TextMeshProUGUI typingLevelText;

        StageDetailData currentData;

        /// <summary>
        /// �X�e�[�W���I�����ꂽ�Ƃ��̃��\�b�h
        /// </summary>
        public void OnSelectStage(StageDetailData stageDetailData)
        {
            // �I�����ꂽ�̂������f�[�^��������ς��Ȃ�
            if (currentData == stageDetailData) { return; }

            currentData = stageDetailData;

            // �f�[�^�̍X�V
            stageDescriptionText.text = stageDetailData.StageDescription;
            kanjiExampleText.text = stageDetailData.KanjiExample;
            kanjiLevelText.text = stageDetailData.KanjiLevel.ToString();
            typingLevelText.text = stageDetailData.TypingLevel.ToString();
        }

    }

}
