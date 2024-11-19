using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextMeshProUGUIButtonSettings : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Color textColorNormal;
    [SerializeField] Color textColorOnPointerEnter;
    [SerializeField] Color textColorOnPointerDown;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        buttonText.color = textColorOnPointerDown;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = textColorOnPointerEnter;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = textColorNormal;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        buttonText.color = textColorNormal;
    }
}