using System.Collections.Generic;
using UnityEngine;

public class RepairUIHandler : MonoBehaviour
{
	private Robot _robot;
	List<InventorySlot> _inventory;

	private void Start()
	{
		_inventory = new List<InventorySlot>();
		for (int i = 0; i < Config.inventoryCapacity; i++)
		{
			_inventory.Add(transform.GetChild(i).gameObject.GetComponent<InventorySlot>());
		}
	}

	public void Show(Robot robot)
	{
		gameObject.SetActive(true);
		_robot = robot;
		PopulateInventory();
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	void PopulateInventory()
	{
		for (int i = 0; i < _inventory.Count; i++)
		{
			_inventory[i].FillSlot(_robot.Inventory[i]);
		}
	}
}