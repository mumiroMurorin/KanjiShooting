using UnityEngine;
using System;

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
