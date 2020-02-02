using System.Collections.Generic;
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
}
