using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private int Damage = 0;
	[SerializeField] private int Score = 0;
	[SerializeField] private GameObject DeathFX;
	
	private void Update()
	{
		transform.position += Vector3.left * Config.levelRunSpeed * Time.deltaTime;
	}

	public int GetDamage()
	{
		return Damage;
	}
	
	public int GetScore()
	{
		return Score;
	}

	public void OnObjectCollided()
	{
		if (DeathFX)
		{
			var fx = Instantiate(DeathFX);
			fx.transform.position = transform.position;
		}
		
		Destroy(gameObject);
	}
}