using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private SparePart _sparePart;

    public SparePart SparePart => _sparePart;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void FillSlot(SparePart sparePart)
    {
        _sparePart = sparePart;
        //TODO Assign the related sprite
        _image.color = SparePart.GetColor(sparePart.Type);
    }
}
