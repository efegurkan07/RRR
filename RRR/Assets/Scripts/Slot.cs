using UnityEngine;

public class Slot : MonoBehaviour
{
    private SparePart _sparePart;

    public SparePart SparePart => _sparePart;

    public void FillSlot(SparePart sparePart)
    {
        this._sparePart = sparePart;
        //TODO Assign the related sprite
    }
}
