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
    [Header("�I�����w�i")]
    [SerializeField] Image selectBack;
    [Header("�X�e�[�W�^�C�g��")]
    [SerializeField] TextMeshProUGUI stageTitleText;
    [Header("��Փx")]
    [SerializeField] TextMeshProUGUI difficulityText;
    [Header("�W������")]
    [SerializeField] TextMeshProUGUI genreText;

    public Action<StageDetailData> OnStageItemButtonClickedListener;
    private StageDetailData stageDetailData;

    /// <summary>
    /// ������
    /// </summary>
    public void Initialize(StageDetailData stageDetailData)
    {
        backGround.sprite = stageDetailData.StageItemBackSprite;
        stageTitleText.text = stageDetailData.StageTitle;
        difficulityText.text = stageDetailData.Difficulity.ToString();
        genreText.text = stageDetailData.Genre;
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
