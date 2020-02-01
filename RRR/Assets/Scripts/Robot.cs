using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Robot : MonoBehaviour
{
	[SerializeField] private GameObject _deathAnimation;
	[SerializeField] private TextMeshPro _healthBar;
	private int _health = 100;
	
	public Lane currentLane; 
	List<SparePart> _inventory; 
	List<BodyPart> _bodyParts;
	

	public List<SparePart> Inventory => _inventory;
	public List<BodyPart> BodyParts => _bodyParts;
	
	private void Start()
	{
		//currentLane = FindObjectsOfType<Lane>().OrderBy(x => Mathf.Abs(x.transform.position.z - transform.position.z))
		//	.First();
		_inventory = new List<SparePart>();
		_bodyParts = new List<BodyPart>();
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.BODY));
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.HORN));
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.TAIL));

		_healthBar.text = _health.ToString();
		
		GameManager.Instance.AddRobot(this);
		
		// TEST
		_inventory.Add(new SparePart(SparePart.SparePartType.RED));
		_inventory.Add(new SparePart(SparePart.SparePartType.BLUE));
		_inventory.Add(new SparePart(SparePart.SparePartType.YELLOW));
		_inventory.Add(new SparePart(SparePart.SparePartType.BLUE));
	}

	public void SetStartLane(Lane lane)
	{
		currentLane = lane;
	}

	private void OnTriggerEnter(Collider other)
	{
		var obstacle = other.GetComponent<Obstacle>();
		if (obstacle != null)
		{
			HandleDamageObstacle(obstacle);
		}
	}

	private void HandleDamageObstacle(Obstacle obstacle)
	{

		var score = obstacle.GetScore();
		var damage = obstacle.GetDamage();
		var sparePart = obstacle.GetSparePart();

		if (sparePart != SparePart.SparePartType.EMPTY)
		{
			AddSparePartToInventory(new SparePart(sparePart));
		}
		
		if (score > 0)
		{
			GameManager.Instance.AddScore(score);
		}

		if (damage > 0)
		{

			_health -= obstacle.GetDamage();
			_healthBar.text = _health.ToString();

			if (_health <= 0)
			{
				GameManager.Instance.RemoveRobot(this);

				if (_deathAnimation != null)
				{
					var anim = Instantiate(_deathAnimation);
					_deathAnimation.transform.position = transform.position;
				}

				Destroy(gameObject);
			}
		}
		
		obstacle.OnObjectCollided();
	}

	private void Update()
	{
		if (currentLane)
		{
			transform.position = Vector3.MoveTowards(
				transform.position,
				new Vector3(transform.position.x, transform.position.y, currentLane.transform.position.z),
				Config.robotRunSpeed * Time.deltaTime
			);
		}
	}

	private bool AddSparePartToInventory(SparePart item)
	{
		if (_inventory.Count >= Config.inventoryCapacity) return false;
		_inventory.Add(item);
		return true;
	}
}