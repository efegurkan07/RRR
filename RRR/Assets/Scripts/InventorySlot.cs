using System;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private SparePart _sparePart;

    public SparePart SparePart => _sparePart;

    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void FillSlot(SparePart sparePart)
    {
        _sparePart = sparePart;
        //TODO Assign the related sprite
        switch (SparePart.Color)
        {
            case SparePart.SparePartColor.RED :
                _sprite.color = Color.red;
                break;
            case SparePart.SparePartColor.BLUE :
                _sprite.color = Color.blue;
                break;
            case SparePart.SparePartColor.YELLOW :
                _sprite.color = Color.yellow;
                break;
            default:
                _sprite.color = Color.gray;
                break;
        }   
    }
}
