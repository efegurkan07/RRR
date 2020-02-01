using System.Collections.Generic;

public class GameManager
{
	private static GameManager _instance;

	private long _lastScore;
	private List<long> _scores = new List<long> {};

	public readonly float TimePerLevel = 10;

	public long LastScore => _lastScore;

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
		_lastScore = 0;
	}

	public void GameOver()
	{
		_scores.Add(_lastScore);
	}

	public long AddScore(long scoreToAdd)
	{
		return _lastScore += scoreToAdd;
	}

	public List<long> GetScores()
	{
		return _scores;
	}
}