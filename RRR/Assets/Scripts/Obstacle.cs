using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private int Damage = 0;
	[SerializeField] private int Score = 0;
	[SerializeField] private SparePart.SparePartType SparePartHandOut = SparePart.SparePartType.EMPTY;
	[SerializeField] private GameObject DeathFX = default;
	
	private AudioSource DeathSound;

	private void Awake()
	{
		DeathSound = GetComponent<AudioSource>();
	}

	private void Start()
	{
		gameObject.AddComponent<Voidable>();
		if (Damage > 0)
		{
			Instantiate(FindObjectOfType<GameHandler>().warningPrefab, gameObject.transform);
		}
		else if (Score > 0)
		{
			Instantiate(FindObjectOfType<GameHandler>().emojiPrefab, gameObject.transform);
		}else if (SparePartHandOut != SparePart.SparePartType.EMPTY)
		{
			Instantiate(FindObjectOfType<GameHandler>().glitterPrefab, gameObject.transform);
		}
	}

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

	public SparePart.SparePartType GetSparePart()
	{
		return SparePartHandOut;
	}

	public void OnObjectCollided()
	{
		if (DeathSound)
		{
			var playSound = GameObject.Find("PlaySound").GetComponent<AudioSource>();
			playSound.clip = DeathSound.clip;
			playSound.Play();
		}
		
		if (DeathFX)
		{
			var fx = Instantiate(DeathFX);
			fx.transform.position = transform.position;
		}
		
		Destroy(gameObject);
	}
}