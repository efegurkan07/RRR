using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
	private float _remainingTime;
	private bool _isGameOver;

	// Start is called before the first frame update
	void Start()
	{
		_remainingTime = GameManager.Instance.TimePerLevel;
		SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);

		GameManager.Instance.StartNewGame();
	}

	private void LateUpdate()
	{
		_remainingTime -= Time.deltaTime;

		if (!_isGameOver && _remainingTime <= 0)
		{
			OnTimeExpired();
		}
	}

	public void AddScore(long scoreToAdd)
	{
		GameManager.Instance.AddScore(scoreToAdd);
	}

	public float GetRemainingTime()
	{
		return _remainingTime;
	}

	public void OnTimeExpired()
	{
		_isGameOver = true;
		GameManager.Instance.GameOver();
		SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
	}
}