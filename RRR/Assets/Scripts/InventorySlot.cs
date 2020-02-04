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

    [SerializeField] Sprite gum = default;
    [SerializeField] Sprite nut = default;
    [SerializeField] Sprite gear = default;

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
        Sprite sprite = GetSprite();
        if (sprite == null)
        {
            _image.enabled = false;
        }
        else
        {
            _image.enabled = true;
            _image.sprite = sprite;
        }
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

    Sprite GetSprite()
    {
        switch (_sparePart.Type)
        {
            case SparePart.SparePartType.RED:
                return gum;
            case SparePart.SparePartType.BLUE:
                return nut;
            case SparePart.SparePartType.YELLOW:
                return gear;
            default:
                return null;
        }
    }
}
