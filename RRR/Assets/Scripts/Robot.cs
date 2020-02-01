using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Robot : MonoBehaviour
{
	public Lane currentLane; 
	List<SparePart> _inventory; 
	List<BodyPart> _bodyParts;
	

	public List<SparePart> Inventory => _inventory;
	public List<BodyPart> BodyParts => _bodyParts;
	
	private void Start()
	{
		currentLane = FindObjectsOfType<Lane>().OrderBy(x => Mathf.Abs(x.transform.position.z - transform.position.z))
			.First();
		_inventory = new List<SparePart>();
		_bodyParts = new List<BodyPart>();
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.BODY_1));
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.BODY_2));
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.HORN_1));
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.HORN_2));
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.TAIL_1));
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.TAIL_2));
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(
			transform.position,
			new Vector3(transform.position.x, transform.position.y, currentLane.transform.position.z),
			Config.robotRunSpeed * Time.deltaTime
		);
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