using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class RepairUIHandler : MonoBehaviour
{
	public static RepairUIHandler instance;
	public static Robot robot;
	
	[SerializeField]
	List<InventorySlot> _inventory; 
	[SerializeField]
	List<BodyPartSlot> _bodyParts;
	
	private void Awake()
	{
		if(instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}
		
		_bodyParts = new List<BodyPartSlot>();
		_inventory = new List<InventorySlot>();
		
		for (int i = 0; i < Config.bodyPartCount ; i++)
		{
			_bodyParts.Add(transform.GetChild(0).GetChild(i + 1).GetComponentInChildren<BodyPartSlot>());
		}
		
		for (int i = 0; i < Config.inventoryCapacity; i++)
		{
			_inventory.Add(transform.GetChild(1).GetChild(i).GetComponentInChildren<InventorySlot>());
		}
	}

	private void OnEnable()
	{
		GameManager.Instance.CurrentState = GameState.RepairOverlay;
	}

	private void OnDisable()
	{
		GameManager.Instance.CurrentState = GameState.DriveUnicorn;
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
		int i = 0;
		foreach (SparePart part in GameManager.Instance.Inventory)
		{
			_inventory[i].FillSlot(part);
			i++;
		}

		for (; i < _inventory.Count; i++)
		{
			_inventory[i].FillSlot(SparePart.EMPTY);
		}
	}

	public void UpdateHealth()
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
	
	
	private static readonly SparePart.SparePartType[] validTypes = {SparePart.SparePartType.RED, SparePart.SparePartType.BLUE, SparePart.SparePartType.YELLOW};
	private static Dictionary<SparePart.SparePartType, int> sparePartCount = null;
	public SparePart.SparePartType GetRandomSparePartType()
	{
		if (sparePartCount == null)
		{
			sparePartCount = new Dictionary<SparePart.SparePartType, int>();
			foreach (SparePart.SparePartType type in validTypes)
			{
				sparePartCount.Add(type, 0);
			}
		}
		
		foreach (BodyPartSlot slot in _bodyParts)
		{
			if(slot.NeededSparePart != SparePart.SparePartType.EMPTY)
				sparePartCount[slot.NeededSparePart]++;
		}

		SparePart.SparePartType part;
		do
		{
			part = validTypes[Random.Range(0, validTypes.Length)];
		} while (sparePartCount[part] > 1);

		foreach (SparePart.SparePartType type in validTypes)
		{
			sparePartCount[type] = 0;
		}
		
		return part;
	}
}