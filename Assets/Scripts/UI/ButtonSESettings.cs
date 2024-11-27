using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Sound;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSESettings : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] SE_Type pointerEnterSEtype;
    [SerializeField] SE_Type pointerDownSEtype;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(!button.interactable) { return; }

        SoundManager.Instance.PlaySE(pointerDownSEtype);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) { return; }

        SoundManager.Instance.PlaySE(pointerEnterSEtype);
    }
}