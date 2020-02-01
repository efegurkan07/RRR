using UnityEngine;

public class SparePart
{
    public static SparePart EMPTY = new SparePart(SparePartType.EMPTY);
    
    public enum SparePartType
    {
        RED = 5,
        BLUE = 10,
        YELLOW = 20,
        EMPTY = 0
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

    public Color GetColor()
    {
        return GetColor(_type);
    }
}
