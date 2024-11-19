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
    [Header("難易度オブジェクト親")]
    [SerializeField] Transform diffcultyParent;
    [Header("ジャンル")]
    [SerializeField] TextMeshProUGUI genreText;
    [Space(30)]
    [Header("ステージデータ")]
    [SerializeField] StageDetailData stageDetailData;
    [Header("難易度イメージオブジェクト")]
    [SerializeField] GameObject diffcultyObject;

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
        genreText.text = stageData.Genre;

        // 難易度(★)のインスタンス化
        for(int i = 0;i< stageData.Difficulty; i++)
        {
            Instantiate(diffcultyObject, diffcultyParent);
        }
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
