using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class RepairUIHandler : MonoBehaviour
{ 
	Robot _robot;
	List<InventorySlot> _inventory; 
	List<BodyPartSlot> _bodyParts;
	
	private void Start()
	{
		_inventory = new List<InventorySlot>();
		_bodyParts = new List<BodyPartSlot>();
		
		for (int i = 0; i < Config.inventoryCapacity; i++)
		{
			_inventory.Add(transform.GetChild(0).GetChild(i).gameObject.GetComponent<InventorySlot>());
		}

		for (int i = 1; i < Config.bodyPartCount; i++)
		{
			_bodyParts.Add(transform.GetChild(1).GetChild(i).GetComponent<BodyPartSlot>());
		}
	}

	public void Show(Robot robot)
	{
		gameObject.SetActive(true);
		_robot = robot;
		PopulateInventory();
		UpdateHealth();
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

	void UpdateHealth()
	{
		foreach (BodyPartSlot slot in _bodyParts)
		{
			slot.Initialize((from bodyPart in _robot.BodyParts where bodyPart.Type == slot.BodyPartType select bodyPart).First());
		}
	}
}