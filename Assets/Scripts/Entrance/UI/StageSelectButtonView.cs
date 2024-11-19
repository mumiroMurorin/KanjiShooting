using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class StageSelectButtonView : MonoBehaviour
{
    public Action OnStageSelectButtonClickedListener;

    /// <summary>
    /// ステージセレクトボタンが押されたときのメソッド
    /// </summary>
    public void OnStageSelectButtonClicked()
    {
        if (OnStageSelectButtonClickedListener != null)
        {
            OnStageSelectButtonClickedListener.Invoke();
        }
    }
}
