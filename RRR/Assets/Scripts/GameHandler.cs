using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
	public Lane[] allLanes;
	[SerializeField] private Material _laneMaterial;
	[SerializeField] private GameObject _obstaclePrefab;
	[SerializeField] private Transform _obstacleContainer;
	[SerializeField] private GameObject jetpackGuy = default;

	void Start()
	{
		GameManager.Instance.StartNewGame();
		SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);
		allLanes = FindObjectsOfType<Lane>();
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
	}

	private void HandleObstacleSpawning()
	{
		GameManager.Instance.secondsToNextObstacles -= Time.deltaTime;
		if (GameManager.Instance.secondsToNextObstacles <= 0)
		{
			var indecies = Enumerable.Range(0, allLanes.Length).OrderBy(x => Random.value).Take(Random.Range(1, allLanes.Length - 1)).ToList();
			for (int i = 0; i < indecies.Count; i++)
			{
				float lineDepth = allLanes[indecies[i]].transform.position.z;
				Instantiate(
					_obstaclePrefab,
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
			OnTimeExpired();
		}
	}

	private void AnimateLanes()
	{
		_laneMaterial.mainTextureOffset =
			new Vector2(_laneMaterial.mainTextureOffset.x - Time.deltaTime * (Config.levelRunSpeed / Config.laneTilingMagicNr), 1);
	}

	private void OnTimeExpired()
	{
		GameManager.Instance.GameOver();
		SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
	}
}