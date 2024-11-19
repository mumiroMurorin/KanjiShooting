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
    [Header("ステージタイトル")]
    [SerializeField] TextMeshProUGUI stageTitleText;
    [Header("難易度")]
    [SerializeField] TextMeshProUGUI difficulityText;
    [Header("ジャンル")]
    [SerializeField] TextMeshProUGUI genreText;
    [Space(30)]
    [Header("ステージデータ")]
    [SerializeField] StageDetailData stageDetailData;

    public Action<StageDetailData> OnStageItemButtonClickedListener;

    private void Start()
    {
        Initialize(stageDetailData);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(StageDetailData stageData)
    {
        backGround.sprite = stageData.StageItemBackSprite;
        stageTitleText.text = stageData.StageTitle;
        difficulityText.text = stageData.Difficulity.ToString();
        genreText.text = stageData.Genre;
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
