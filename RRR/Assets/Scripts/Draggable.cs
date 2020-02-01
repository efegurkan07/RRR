using UnityEngine;

using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private bool dragging;

    private Vector2 initialPosition;
    private RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
   
    
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.position = initialPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = rectTransform.position;
    }
}