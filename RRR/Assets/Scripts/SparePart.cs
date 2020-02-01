using UnityEngine;

public class SparePart
{
    public enum SparePartColor
    {
        RED,
        BLUE,
        YELLOW,
        EMPTY
    }

    [SerializeField]
    private SparePartColor _color;

    public SparePartColor Color
    {
        get => _color;
        set
        {
            _color = value;
            //TODO add names etc.
        }
    }

    public SparePart(SparePartColor color)
    {
        _color = color;
    }
}
