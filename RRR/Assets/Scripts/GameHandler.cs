using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
	private float _remainingTime;
	private bool _isGameOver;

	// Start is called before the first frame update
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
	}

	private void OnTimeExpired()
	{
		GameManager.Instance.GameOver();
		SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
	}
}