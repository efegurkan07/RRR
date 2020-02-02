using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class BodyPartSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] Sprite gum;
    [SerializeField] Sprite nut;
    [SerializeField] Sprite gear;

    private Image _image;
    private HealthBarHandlerUI _healthBarHandlerUi;
    
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

    // SparePart.SparePartType SparePartType
    // {
    //     get => _sparePartType; 
    //     set
    //     {
    //         _sparePartType = value;
    //         _image.color = SparePart.GetColor(value);
    //     }
    // }

    public void Awake()
    {
        _image = transform.GetChild(1).GetComponent<Image>();
        _healthBarHandlerUi = GetComponentInChildren<HealthBarHandlerUI>();
    }

    public void Initialize(BodyPart bodyPart)
    {
        type = bodyPart.Type;
        _bodyPart = bodyPart;
        // SparePartType = SparePart.SparePartType.EMPTY;
        _image.enabled = false;
    }
    
    void Repair(SparePart sparePart)
    {
        _sparePartType = sparePart.Type;
        _image.sprite = GetSprite(sparePart);
        _image.enabled = true;
        _bodyPart.Repair(sparePart);
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot inventorySlot = InventorySlot.itemBeingDragged;
        if (inventorySlot != null)
        {
            SparePart part = inventorySlot.SparePart;
            Repair(part);
            RepairUIHandler.robot.UpdateHealth();
            inventorySlot.FillSlot(SparePart.EMPTY);
        }
    }

    void Update()
    {
        _healthBarHandlerUi.SetHealth(_bodyPart.Health);
    }
    
    public Sprite GetSprite(SparePart part)
    {
        switch (part.Type)
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
