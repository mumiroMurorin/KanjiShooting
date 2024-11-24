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
    /// ì«Ç›çûÇ›UIîÒï\é¶âª
    /// </summary>
    public void OnFinishLoading()
    {

    }

    /// <summary>
    /// ì«Ç›çûÇ›UIï\é¶
    /// </summary>
    public void OnStartLoading()
    {

    }
}