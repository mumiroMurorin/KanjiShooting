using UnityEngine;
using System;

public class OptionButtonView : MonoBehaviour
{
    public Action OnOptionButtonClickedListener;

    /// <summary>
    /// �ݒ�{�^���������ꂽ�Ƃ��̃��\�b�h
    /// </summary>
    public void OnOptionButtonClicked()
    {
        if (OnOptionButtonClickedListener != null)
        {
            OnOptionButtonClickedListener.Invoke();
        }
    }
}