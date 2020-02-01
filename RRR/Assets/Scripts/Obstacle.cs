using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private int Damage = 10;
	
	private void Update()
	{
		transform.position += Vector3.left * Config.levelRunSpeed * Time.deltaTime;
	}

	public int GetDamage()
	{
		return Damage;
	}
}