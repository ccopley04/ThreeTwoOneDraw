using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Sprite currSprite;
    [SerializeField]
    private Image displayRenderer;
    private Color thisColor;

    void Start()
    {
        currSprite = gameObject.GetComponent<Image>().sprite;
        thisColor = displayRenderer.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        displayRenderer.sprite = gameObject.GetComponent<Image>().sprite;
        thisColor.a = 1f;
        displayRenderer.color = thisColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        displayRenderer.sprite = null;
        thisColor.a = 0f;
        displayRenderer.color = thisColor;
    }


}
