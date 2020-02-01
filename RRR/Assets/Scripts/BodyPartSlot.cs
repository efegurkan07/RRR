using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class BodyPartSlot : MonoBehaviour, IDropHandler
{
    private Image _image;

    [SerializeField]
    private BodyPart.BodyPartType type;

    public BodyPart.BodyPartType BodyPartType
    {
        get => type;
    }

    private SparePart.SparePartType _sparePartType;
    private BodyPart _bodyPart;

    public BodyPart BodyPart => _bodyPart;

    float Health => _bodyPart.Health;

    SparePart.SparePartType SparePartType
    {
        get => _sparePartType; 
        set
        {
            _sparePartType = value;
            _image.color = SparePart.GetColor(value);
        }
    }

    public void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Initialize(BodyPart bodyPart)
    {
        type = bodyPart.Type;
        _bodyPart = bodyPart;
        SparePartType = SparePart.SparePartType.EMPTY;
    }
    
    void Repair(SparePart sparePart)
    {
        _sparePartType = sparePart.Type;
        Debug.Log("Repairing " + type + " with " + _sparePartType);
        _image.color = sparePart.GetColor();
        _bodyPart.Repair(sparePart);
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
