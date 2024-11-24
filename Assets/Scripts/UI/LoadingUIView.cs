using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUIView : MonoBehaviour
{
    [SerializeField] bool PlayOnAwake;
    [SerializeField] Image backGround;

    private void Awake()
    {
        if (PlayOnAwake)
        {
            SetActive(true);
        }
    }

    public void SetActive(bool isActive)
    {
        backGround.raycastTarget = !isActive;
        backGround.fillAmount = isActive ? 1 : 0;
    }

    /// <summary>
    /// �ǂݍ���UI��\����
    /// </summary>
    public void OnFinishLoading()
    {

    }

    /// <summary>
    /// �ǂݍ���UI�\��
    /// </summary>
    public void OnStartLoading()
    {

    }
}