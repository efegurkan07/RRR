using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[Serializable]
public class ObstacleSpawnConfig
{
	public Obstacle obstaclePrefab;
	public int Probability;
}

public class GameHandler : MonoBehaviour
{
	public Lane[] allLanes;
	[SerializeField] private ObstacleSpawnConfig[] _obstacleSpawnConfigs;
	[SerializeField] private ObstacleSpawnConfig[] _sparePartsSpawnConfigs;
	[SerializeField] private ObstacleSpawnConfig[] _peopleSpawnConfigs;
	[SerializeField] private Transform _obstacleContainer;
	[SerializeField] private GameObject jetpackGuy = default;

	[SerializeField] private Robot _robotPrefab;
	public GameObject warningPrefab;
	public GameObject emojiPrefab;
	public GameObject glitterPrefab;

	private ObjectSpawner _objectSpawner;
	
	private void Awake()
	{
		_objectSpawner = new ObjectSpawner(_obstacleSpawnConfigs, _sparePartsSpawnConfigs, _peopleSpawnConfigs);
	}

	void Start()
	{
		GameManager.Instance.StartNewGame();
		MusicHandler.instance.insideLoop = true;
		SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);
		allLanes = FindObjectsOfType<Lane>();

		var robot = Instantiate(_robotPrefab);
		var startLanel = FindObjectsOfType<Lane>().OrderBy(x => Mathf.Abs(x.transform.position.z - robot.transform.position.z)).First();
		robot.SetStartLane(startLanel);

		foreach (var lane in allLanes)
		{
			var material = lane.GetComponent<MeshRenderer>().material;
			material.mainTextureOffset = new Vector2(Random.value, 1f);
		}
		
		/*var robot2 = Instantiate(_robotPrefab);
		robot2.SetStartLane(allLanes[1]);
		
		var robot3 = Instantiate(_robotPrefab);
		robot3.SetStartLane(allLanes[2]);*/
		
		StartCoroutine(DamageOverTime());
	}

	public void LaunchJetpack()
	{
		Robot robot = FindObjectOfType<Robot>();
		var poorGuy = Instantiate(jetpackGuy);
		poorGuy.transform.position = robot.transform.position;
		poorGuy.GetComponent<Animator>().SetTrigger("HeavensCalling");
	}

	private void Update()
	{
		AnimateLanes();
		HandleObstacleSpawning();

		if (!GameManager.Instance.HasRobots())
		{
			OnGameOver();
		}

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			TogglePause();
		}
	}

	private void TogglePause()
	{
		if (Time.timeScale > 0)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
	
	private void HandleObstacleSpawning()
	{
		GameManager.Instance.secondsToNextObstacles -= Time.deltaTime;
		if (GameManager.Instance.secondsToNextObstacles <= 0)
		{
			var indecies = Enumerable.Range(0, allLanes.Length).OrderBy(x => Random.value).Take(Random.Range(1, allLanes.Length - 1)).ToList();
			for (int i = 0; i < indecies.Count; i++)
			{
				var lineDepth = allLanes[indecies[i]].transform.position.z;

				var nextObstacle = _objectSpawner.NextObjectToSpawn();
				
				Instantiate(
					nextObstacle.gameObject,
					new Vector3(Config.rightLineLimit, 0, lineDepth),
					nextObstacle.transform.rotation,
					_obstacleContainer
				);
			}

			GameManager.Instance.secondsToNextObstacles = Random.Range(0.5f, 1f) * Config.levelRunSpeed;
		}
	}

	private void AnimateLanes()
	{
		foreach (var lane in allLanes)
		{
			var material = lane.GetComponent<MeshRenderer>().material;
			material.mainTextureOffset =
				new Vector2((material.mainTextureOffset.x - Time.deltaTime * (Config.levelRunSpeed / Config.laneTilingMagicNr)) % 1, 1);
		}
	}

	private void OnGameOver()
	{
		GameManager.Instance.GameOver();
		MusicHandler.instance.insideLoop = false;
		SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
		StopCoroutine(DamageOverTime());
	}

	IEnumerator DamageOverTime()
	{
		while (true)
		{
			GameManager.Instance.DamageAllRobots(Config.damageOverTime);
			yield return Config.damageOverTimePeriod;
		}
	}
}