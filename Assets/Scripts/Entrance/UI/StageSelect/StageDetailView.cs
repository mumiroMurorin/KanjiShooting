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
        [Space(20)]
        [Header("�������x���I�u�W�F�N�g�e")]
        [SerializeField] Transform kanjiLevelParent;
        [Header("�������x���C���[�W�I�u�W�F�N�g")]
        [SerializeField] GameObject kanjiLevelObject;
        [Space(20)]
        [Header("�^�C�s���O���x���I�u�W�F�N�g�e")]
        [SerializeField] Transform typingLevelParent;
        [Header("�^�C�s���O���x���C���[�W�I�u�W�F�N�g")]
        [SerializeField] GameObject typingLevelObject;

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

            SetKanjiLevel(stageDetailData);
            SetTypingLevel(stageDetailData.TypingLevel);
        }

        /// <summary>
        /// �������x���̃Z�b�g
        /// </summary>
        private void SetKanjiLevel(StageDetailData stageDetailData)
        {
            // �S�폜
            if(kanjiLevelParent.childCount > 0)
            {
                foreach (Transform child in kanjiLevelParent.transform) { Destroy(child.gameObject); }
            }

            // ��Փx(��)�̃C���X�^���X��
            for (int i = 0; i < stageDetailData.KanjiLevel; i++)
            {
                Instantiate(kanjiLevelObject, kanjiLevelParent);
            }

        }

        /// <summary>
        /// �^�C�s���O���x���̃Z�b�g
        /// </summary>
        private void SetTypingLevel(int level)
        {
            // �S�폜
            if (kanjiLevelParent.childCount > 0)
            {
                foreach (Transform child in typingLevelParent.transform) { Destroy(child.gameObject); }
            }

            // ��Փx(��)�̃C���X�^���X��
            for (int i = 0; i < level; i++)
            {
                Instantiate(typingLevelObject, typingLevelParent);
            }
        }
    }

}
