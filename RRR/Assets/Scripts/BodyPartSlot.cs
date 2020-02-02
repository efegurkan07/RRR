using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class BodyPartSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] Sprite gum;
    [SerializeField] Sprite nut;
    [SerializeField] Sprite gear;

    // [SerializeField]
    // private Image itemImage;
    [SerializeField]
    private Image neededItemImage;
    [SerializeField]
    private HealthBarHandlerUI _healthBarHandlerUi;
    [SerializeField]
    private BodyPart.BodyPartType type;

    [SerializeField] private AudioSource itemAppliedSound;

    private bool repairable = true;
    
    [SerializeField] private GameObject outOfOrderText;

    public BodyPart.BodyPartType BodyPartType
    {
        get => type;
    }

    private SparePart.SparePartType _neededSparePart = SparePart.SparePartType.EMPTY;

    public SparePart.SparePartType  NeededSparePart
    {
        get => _neededSparePart;
        set
        {
            _neededSparePart = value;
            neededItemImage.sprite = GetSprite(value);
        }
    }

    private BodyPart _bodyPart;

    public void Start()
    {
        repairable = true;
    }
    
    public void Initialize(BodyPart bodyPart)
    {
        type = bodyPart.Type;
        _bodyPart = bodyPart;
        if(NeededSparePart == SparePart.SparePartType.EMPTY)
            NeededSparePart = RepairUIHandler.instance.GetRandomSparePartType();
        // itemImage.enabled = false;
    }
    
    void Repair(SparePart sparePart)
    {
        // itemImage.sprite = GetSprite(sparePart.Type);
        // itemImage.enabled = true;
        _bodyPart.Repair(sparePart);
        NeededSparePart = RepairUIHandler.instance.GetRandomSparePartType();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!repairable) return;
        
        InventorySlot inventorySlot = InventorySlot.itemBeingDragged;
        
        if (inventorySlot == null) return;
        
        SparePart part = inventorySlot.SparePart;
        if (part.Type != _neededSparePart) return;
        
        Repair(part);
        RepairUIHandler.robot.UpdateHealth();
        
        itemAppliedSound.Play();
    }

    void Update()
    {
        if (!repairable) return;
        if (_bodyPart == null) return;
        if (_bodyPart.Health > 0)
        {
            _healthBarHandlerUi.SetHealth(_bodyPart.Health);
        }
        else
        {
            Brake();
        }
    }
    
    public Sprite GetSprite(SparePart.SparePartType type)
    {
        switch (type)
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

    void Brake()
    {
        repairable = false;
        outOfOrderText.SetActive(true);
        _neededSparePart = SparePart.SparePartType.EMPTY;
        RectTransform rectTransform = (RectTransform)outOfOrderText.transform;
        rectTransform.Rotate(new Vector3(0, Random.Range(-20f, 20f), 0f));
        _healthBarHandlerUi.gameObject.SetActive(false);
    }
}
