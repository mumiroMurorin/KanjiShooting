using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageImageBackView : MonoBehaviour
{
    [Header("�w�i")]
    [SerializeField] Image backGroundImage;

    /// <summary>
    /// �X�e�[�W�I����
    /// </summary>
    public void OnSelectStage(Sprite sprite)
    {
        if (backGroundImage.sprite == sprite) { return; }
        backGroundImage.sprite = sprite;
    }
}
