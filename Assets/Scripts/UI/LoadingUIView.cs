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
    /// 読み込みUI非表示化
    /// </summary>
    public void OnFinishLoading()
    {

    }

    /// <summary>
    /// 読み込みUI表示
    /// </summary>
    public void OnStartLoading()
    {

    }
}