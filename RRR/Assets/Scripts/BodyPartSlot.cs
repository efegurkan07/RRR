using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class BodyPartSlot : MonoBehaviour, IDropHandler
{
    private Image image;

    [SerializeField]
    private BodyPart.BodyPartType type;

    public BodyPart.BodyPartType BodyPartType
    {
        get => type;
    }

    private float _health;
    private SparePart.SparePartType _sparePartType;
    
    float Health
    {
        get => _health;
        set => _health = value;
    }
    
    SparePart.SparePartType SparePartType
    {
        get => _sparePartType; 
        set
        {
            _sparePartType = value;
            image.color = SparePart.GetColor(value);
        }
    }

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Initialize(BodyPart bodyPart)
    {
        type = bodyPart.Type;
        _health = bodyPart.Health;
    }
    
    public void Repair(SparePart sparePart)
    {
        _sparePartType = sparePart.Type;
        Debug.Log("Repairing " + type + " with " + _sparePartType);
        image.color = sparePart.GetColor();
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot inventorySlot = InventorySlot.itemBeingDragged;
        if (inventorySlot != null)
        {
            SparePart part = inventorySlot.SparePart;
            Repair(part);
            inventorySlot.FillSlot(SparePart.EMPTY);
        }

    }
}
