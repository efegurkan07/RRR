using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepairUIHandler : MonoBehaviour
{ 
	public static Robot robot;
	
	[SerializeField]
	List<InventorySlot> _inventory; 
	[SerializeField]
	List<BodyPartSlot> _bodyParts;
	
	private void Awake()
	{
		_bodyParts = new List<BodyPartSlot>();
		_inventory = new List<InventorySlot>();
		
		for (int i = 0; i < Config.bodyPartCount ; i++)
		{
			_bodyParts.Add(transform.GetChild(0).GetChild(i + 1).GetComponent<BodyPartSlot>());
		}
		
		for (int i = 0; i < Config.inventoryCapacity; i++)
		{
			_inventory.Add(transform.GetChild(1).GetChild(i).gameObject.GetComponent<InventorySlot>());
		}
	}

	public void Show(Robot robotToBeRepaired)
	{
		gameObject.SetActive(true);
		robot = robotToBeRepaired;
		PopulateInventory();
		UpdateHealth();
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	void PopulateInventory()
	{
		int i;
		int upperBound = Mathf.Min(robot.Inventory.Count, _inventory.Count);
		for (i = 0; i < upperBound; i++)
		{
			_inventory[i].FillSlot(robot.Inventory[i]);
		}

		for (; i < _inventory.Count; i++)
		{
			_inventory[i].FillSlot(SparePart.EMPTY);
		}
	}

	void UpdateHealth()
	{
		foreach (BodyPartSlot slot in _bodyParts)
		{
			slot.Initialize((from bodyPart in robot.BodyParts where bodyPart.Type == slot.BodyPartType select bodyPart).First());
		}
	}

	private void Update()
	{
		PopulateInventory();
	}
}