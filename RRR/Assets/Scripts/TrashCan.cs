using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot inventorySlot = InventorySlot.itemBeingDragged;
        
        GameManager.Instance.Inventory.Remove(inventorySlot.SparePart);
    }
}
