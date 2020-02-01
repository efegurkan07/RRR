using UnityEngine;
using UnityEngine.UI;

public class BodyPartSlot : MonoBehaviour
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

    public void Initialize(BodyPart bodyPart)
    {
        type = bodyPart.Type;
        _health = bodyPart.Health;
    }
    
    public void Repair(SparePart sparePart)
    {
        _sparePartType = sparePart.Type;
    }
}
