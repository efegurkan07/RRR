using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Robot : MonoBehaviour
{
	public Lane currentLane; 
	List<Collectible> inventory;
	
	private void Start()
	{
		currentLane = FindObjectsOfType<Lane>().OrderBy(x => Mathf.Abs(x.transform.position.z - transform.position.z))
			.First();
		inventory = new List<Collectible>();
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(
			transform.position,
			new Vector3(transform.position.x, transform.position.y, currentLane.transform.position.z),
			Time.deltaTime
		);
	}

	private bool AddCollectibleToInventory(Collectible item)
	{
		if (inventory.Count >= Config.inventoryCapacity)
		{
			inventory.Add(item);
			return true;
		}

		return false;
	}
}