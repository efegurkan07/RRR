using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static InventorySlot itemBeingDragged;
    
    [SerializeField]
    private SparePart _sparePart;
    private bool dragging;
    private Vector2 initialPosition;
    private RectTransform rectTransform;
    private Image _image;
    public SparePart SparePart => _sparePart;


    private void Awake()
    {
        _image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        FillSlot(SparePart.EMPTY);
    }

    public void FillSlot(SparePart sparePart)
    {
        _sparePart = sparePart;
        //TODO Assign the related sprite, name , whatever
        _image.color = sparePart.GetColor();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        rectTransform.position = initialPosition;
        GetComponentInParent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_sparePart.Type != SparePart.SparePartType.EMPTY)
        {
            itemBeingDragged = this;
            initialPosition = rectTransform.position;
            GetComponentInParent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            eventData.pointerDrag = null;
        }
    }
}
