using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Robot : MonoBehaviour
{
	public Lane currentLane; 
	List<SparePart> _inventory;

	public List<SparePart> Inventory => _inventory;

	private void Start()
	{
		currentLane = FindObjectsOfType<Lane>().OrderBy(x => Mathf.Abs(x.transform.position.z - transform.position.z))
			.First();
		_inventory = new List<SparePart>();
	}

	private void Update()
	{
		var position = transform.position;
		position = Vector3.Lerp(
			position,
			new Vector3(position.x, position.y, currentLane.transform.position.z),
			Time.deltaTime
		);
		transform.position = position;
	}

	private bool AddCollectibleToInventory(SparePart item)
	{
		if (_inventory.Count >= Config.inventoryCapacity)
		{
			_inventory.Add(item);
			return true;
		}

		return false;
	}
}