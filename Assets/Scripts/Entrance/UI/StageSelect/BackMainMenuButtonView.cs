using UnityEngine;
using System;

public class BackMainMenuButtonView : MonoBehaviour
{
    public Action OnBackMainMenuButtonClickedListener;

    /// <summary>
    /// 「メインメニューに戻る」ボタンが押されたときのメソッド
    /// </summary>
    public void OnBackMainMenuButtonClicked()
    {
        if (OnBackMainMenuButtonClickedListener != null)
        {
            OnBackMainMenuButtonClickedListener.Invoke();
        }
    }
}