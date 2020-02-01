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

		if (!_isGameOver && GameManager.Instance.remainingTime <= 0)
		{
			OnTimeExpired();
		}
	}

	private void OnTimeExpired()
	{
		_isGameOver = true;
		GameManager.Instance.GameOver();
		SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
	}
}