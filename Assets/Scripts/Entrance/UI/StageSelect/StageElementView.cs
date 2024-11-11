using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StageElementView : MonoBehaviour
{
    [Header("背景")]
    [SerializeField] Image backGround;
    [Header("選択時背景")]
    [SerializeField] Image selectBack;
    [Header("ステージタイトル")]
    [SerializeField] TextMeshProUGUI stageTitleText;
    [Header("難易度")]
    [SerializeField] TextMeshProUGUI difficulityText;
    [Header("ジャンル")]
    [SerializeField] TextMeshProUGUI genreText;

    public Action<StageDetailData> OnStageItemButtonClickedListener;
    private StageDetailData stageDetailData;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(StageDetailData stageDetailData)
    {
        backGround.sprite = stageDetailData.StageItemBackSprite;
        stageTitleText.text = stageDetailData.StageTitle;
        difficulityText.text = stageDetailData.Difficulity.ToString();
        genreText.text = stageDetailData.Genre;
    }

    /// <summary>
    /// 選択されたときのメソッド
    /// </summary>
    public void OnSelect()
    {
        
    }

    /// <summary>
    /// 他が選択されたときのメソッド
    /// </summary>
    public void OnDeselect()
    {

    }

    /// <summary>
    /// ステージボタンがクリックされたとき
    /// </summary>
    public void OnStageItemButtonClicked()
    {
        if(OnStageItemButtonClickedListener != null)
        {
            OnStageItemButtonClickedListener.Invoke(stageDetailData);
        }
    }
}
