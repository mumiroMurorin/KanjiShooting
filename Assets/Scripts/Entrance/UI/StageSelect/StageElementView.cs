using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StageElementView : MonoBehaviour
{
    [Header("�w�i")]
    [SerializeField] Image backGround;
    [Header("�X�e�[�W�^�C�g��")]
    [SerializeField] TextMeshProUGUI stageTitleText;
    [Header("��Փx")]
    [SerializeField] TextMeshProUGUI difficulityText;
    [Header("�W������")]
    [SerializeField] TextMeshProUGUI genreText;
    [Space(30)]
    [Header("�X�e�[�W�f�[�^")]
    [SerializeField] StageDetailData stageDetailData;

    public Action<StageDetailData> OnStageItemButtonClickedListener;

    private void Start()
    {
        Initialize(stageDetailData);
    }

    /// <summary>
    /// ������
    /// </summary>
    public void Initialize(StageDetailData stageData)
    {
        backGround.sprite = stageData.StageItemBackSprite;
        stageTitleText.text = stageData.StageTitle;
        difficulityText.text = stageData.Difficulity.ToString();
        genreText.text = stageData.Genre;
    }

    /// <summary>
    /// �I�����ꂽ�Ƃ��̃��\�b�h
    /// </summary>
    public void OnSelect()
    {
        
    }

    /// <summary>
    /// �����I�����ꂽ�Ƃ��̃��\�b�h
    /// </summary>
    public void OnDeselect()
    {

    }

    /// <summary>
    /// �X�e�[�W�{�^�����N���b�N���ꂽ�Ƃ�
    /// </summary>
    public void OnStageItemButtonClicked()
    {
        if(OnStageItemButtonClickedListener != null)
        {
            OnStageItemButtonClickedListener.Invoke(stageDetailData);
        }
    }
}
