using UnityEngine;

public class SparePart
{
    public enum SparePartType
    {
        RED,
        BLUE,
        YELLOW,
        EMPTY
    }

    [SerializeField]
    private SparePartType _type;

    public SparePartType Type
    {
        get => _type;
        set
        {
            _type = value;
            //TODO add names etc.
        }
    }

    public SparePart(SparePartType type)
    {
        _type = type;
    }

    public static Color GetColor(SparePartType sparePartType)
    {
        switch (sparePartType)
        {
            case SparePartType.RED :
                return Color.red;
            case SparePartType.BLUE :
                return Color.blue;
            case SparePartType.YELLOW :
                return Color.yellow;
            default:
                return Color.grey;
        }   
    }
}
