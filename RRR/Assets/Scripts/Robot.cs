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

	private float Health
	{
		get
		{
			float health = 0;
			foreach (BodyPart part in BodyParts)
			{
				health += part.Health;
			}

			return health / BodyParts.Count;
		}
	}
	
	public Lane currentLane; 
	List<BodyPart> _bodyParts;
	
	public List<BodyPart> BodyParts => _bodyParts;
	
	private void Start()
	{
		//currentLane = FindObjectsOfType<Lane>().OrderBy(x => Mathf.Abs(x.transform.position.z - transform.position.z))
		//	.First();
		_bodyParts = new List<BodyPart>();
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.BODY));
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.HORN));
		_bodyParts.Add(new BodyPart(BodyPart.BodyPartType.TAIL));

		_healthBar.text = Health.ToString();
		
		GameManager.Instance.AddRobot(this);
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
		var consumed = false;

		if (sparePart != SparePart.SparePartType.EMPTY)
		{
			consumed = AddSparePartToInventory(new SparePart(sparePart));
			
		}
		
		if (score > 0)
		{
			GameManager.Instance.AddScore(score);
			consumed = true;
		}

		if (damage > 0)
		{
			Camera.main.GetComponent<ShakeBehavior>().TriggerShake(0.2f, 0.3f);
			foreach (BodyPart part in BodyParts)
			{ 
				part.GetDamaged(damage);
			}
			_healthBar.text = Health.ToString();

			if (Health <= 0)
			{
				GameManager.Instance.RemoveRobot(this);

				if (_deathAnimation != null)
				{
					var anim = Instantiate(_deathAnimation);
					_deathAnimation.transform.position = transform.position;
				}

				Destroy(gameObject);
			}
			consumed = true;
		}

		if (consumed)
		{
			obstacle.OnObjectCollided();
		}
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
		if (GameManager.Instance.Inventory.Count >= Config.inventoryCapacity)
		{
			return false;
		}
		
		GameManager.Instance.Inventory.Add(item);
		return true;
	}
}