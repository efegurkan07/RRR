using System.Collections.Generic;

public class GameManager
{
	private static GameManager _instance;

	public float remainingTime;
	public long lastScore;
	public List<long> scores = new List<long>();

	public long LastScore => lastScore;

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
	}

	public void GameOver()
	{
		scores.Add(lastScore);
	}

	public void AddScore(long scoreToAdd)
	{
		lastScore += scoreToAdd;
	}
}