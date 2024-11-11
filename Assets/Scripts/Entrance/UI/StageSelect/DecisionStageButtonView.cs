using UnityEngine;
using System;

public class DecisionStageButtonView : MonoBehaviour
{
    public Action OnDecisionStageButtonClickedListener;

    /// <summary>
    /// 出撃ボタンが押されたときのメソッド
    /// </summary>
    public void OnDecisionStageButtonClicked()
    {
        if (OnDecisionStageButtonClickedListener != null)
        {
            OnDecisionStageButtonClickedListener.Invoke();
        }
    }
}