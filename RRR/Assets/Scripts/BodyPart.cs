using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BodyPart
{
    public enum BodyPartType
    {
        HORN,
        BODY,
        TAIL
    }

    private int _health;

    public int Health
    {
        get { return _health; }
    }

    [SerializeField]
    private BodyPartType _type;
    public BodyPartType Type => _type;

    private SparePart _lastSparePartUsed;
    public SparePart LastSparePartUsed => _lastSparePartUsed;

    public BodyPart(BodyPartType type)
    {
        _type = type;
        _health = Config.initialBodyPartHealth;
        _lastSparePartUsed = SparePart.EMPTY;
    }

    public void Repair(SparePart sparePart)
    {
        _lastSparePartUsed = sparePart;
        GameManager.Instance.Inventory.Remove(sparePart);
        //TODO Update Health
        _health += (int) sparePart.Type;
    }

    public void GetDamaged(int damage)
    {
        _health -= damage;
    }
}
