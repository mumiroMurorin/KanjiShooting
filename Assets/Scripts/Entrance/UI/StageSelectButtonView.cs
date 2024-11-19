using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class StageSelectButtonView : MonoBehaviour
{
    public Action OnStageSelectButtonClickedListener;

    /// <summary>
    /// �X�e�[�W�Z���N�g�{�^���������ꂽ�Ƃ��̃��\�b�h
    /// </summary>
    public void OnStageSelectButtonClicked()
    {
        if (OnStageSelectButtonClickedListener != null)
        {
            OnStageSelectButtonClickedListener.Invoke();
        }
    }
}
