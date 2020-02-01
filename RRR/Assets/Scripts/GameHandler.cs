using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
	[SerializeField] private Material laneMaterial;

	void Start()
	{
		GameManager.Instance.StartNewGame();
		SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);
	}

	private void Update()
	{
		GameManager.Instance.remainingTime -= Time.deltaTime;

		if (!GameManager.Instance.IsGameOver && GameManager.Instance.remainingTime <= 0)
		{
			OnTimeExpired();
		}

		AnimateLanes();
	}

	private void AnimateLanes()
	{
		laneMaterial.mainTextureOffset =
			new Vector2(laneMaterial.mainTextureOffset.x - Time.deltaTime * Config.laneRunSpeed, 1);
	}

	private void OnTimeExpired()
	{
		GameManager.Instance.GameOver();
		SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
	}
}