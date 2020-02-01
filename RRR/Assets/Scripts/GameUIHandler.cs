using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _scoreLabel;
	[SerializeField] private TextMeshProUGUI _timeLabel;

	private GameHandler _gameHandler;

	private void Awake()
	{
		_gameHandler = FindObjectOfType<GameHandler>();
	}

	private void Update()
	{
		_scoreLabel.text = GameManager.Instance.LastScore.ToString();
		UpdateTimeLabel(_gameHandler.GetRemainingTime());
	}

	public void GotoMainMenu()
	{
		SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
	}

	public void AddScore()
	{
		_gameHandler.AddScore(1);
	}

	private void UpdateTimeLabel(float remainingTime)
	{
		int minutes = (int) (remainingTime / 60);
		var seconds = (remainingTime % 60);

		_timeLabel.text = string.Format("{0}:{1:00.0}", minutes, seconds);
	}
}