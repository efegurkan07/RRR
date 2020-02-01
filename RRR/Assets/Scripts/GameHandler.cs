using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
	public Lane[] allLanes;
	[SerializeField] private Material _laneMaterial;
	[SerializeField] private Obstacle[] _obstaclePrefabs;
	[SerializeField] private Transform _obstacleContainer;
	[SerializeField] private GameObject jetpackGuy = default;

	[SerializeField] private Robot _robotPrefab;

	
	void Start()
	{
		GameManager.Instance.StartNewGame();
		SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);
		allLanes = FindObjectsOfType<Lane>();

		var robot = Instantiate(_robotPrefab);
		robot.SetStartLane(allLanes[1]);
		
		/*var robot2 = Instantiate(_robotPrefab);
		robot2.SetStartLane(allLanes[1]);
		
		var robot3 = Instantiate(_robotPrefab);
		robot3.SetStartLane(allLanes[2]);*/
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
		HandleGameTime();
		AnimateLanes();
		HandleObstacleSpawning();

		if (!GameManager.Instance.HasRobots())
		{
			OnGameOver();
		}
	}

	private Random random = new Random();
	
	private void HandleObstacleSpawning()
	{
		GameManager.Instance.secondsToNextObstacles -= Time.deltaTime;
		if (GameManager.Instance.secondsToNextObstacles <= 0)
		{
			var indecies = Enumerable.Range(0, allLanes.Length).OrderBy(x => Random.value).Take(Random.Range(1, allLanes.Length - 1)).ToList();
			for (int i = 0; i < indecies.Count; i++)
			{
				float lineDepth = allLanes[indecies[i]].transform.position.z;
				int randomIndex = Random.Range(0, _obstaclePrefabs.Length - 1);
				var randomObstacle = _obstaclePrefabs[randomIndex];
				Instantiate(
					randomObstacle.gameObject,
					new Vector3(Config.rightLineLimit, 0, lineDepth),
					Quaternion.Euler(Vector3.zero),
					_obstacleContainer
				);
			}

			GameManager.Instance.secondsToNextObstacles = Random.Range(0.5f, 1f) * Config.levelRunSpeed;
		}
	}

	private void HandleGameTime()
	{
		GameManager.Instance.remainingTime -= Time.deltaTime;

		if (!GameManager.Instance.IsGameOver && GameManager.Instance.remainingTime <= 0)
		{
			OnGameOver();
		}
	}

	private void AnimateLanes()
	{
		_laneMaterial.mainTextureOffset =
			new Vector2((_laneMaterial.mainTextureOffset.x - Time.deltaTime * (Config.levelRunSpeed / Config.laneTilingMagicNr)) % 1, 1);
	}

	private void OnGameOver()
	{
		GameManager.Instance.GameOver();
		SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
	}

	private void OnDestroy()
	{
		_laneMaterial.mainTextureOffset = Vector2.one;
	}
}