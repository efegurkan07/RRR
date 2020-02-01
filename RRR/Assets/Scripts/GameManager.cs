using System.Collections.Generic;

public class GameManager
{
	private static GameManager _instance;

	public float remainingTime;
	public long lastScore;
	public List<HighScoreEntry> scores = new List<HighScoreEntry>();

	public long LastScore => lastScore;
	public bool IsGameOver;
	public float secondsToNextObstacles;

	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameManager();
			}

			return _instance;
		}
	}

	public void StartNewGame()
	{
		remainingTime = Config.secondsPerLevel;
		lastScore = 0;
		IsGameOver = false;
	}

	public void GameOver()
	{
		IsGameOver = true;
		scores.Add(new HighScoreEntry()
		{
			Score = LastScore
		});
	}

	public void AddScore(long scoreToAdd)
	{
		lastScore += scoreToAdd;
	}
}