using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;

public class GameManager
{
	private static GameManager _instance;

	public long lastScore;
	public List<HighScoreEntry> scores = new List<HighScoreEntry>();

	public long LastScore => lastScore;
	public bool IsGameOver;
	public float secondsToNextObstacles;
	
	public List<SparePart> Inventory = new List<SparePart>();
	private List<Robot> _robots = new List<Robot>();

	public GameState CurrentState = GameState.MainMenu;

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
		lastScore = 0;
		IsGameOver = false;

		CurrentState = GameState.DriveUnicorn;
	}

	public void GameOver()
	{
		IsGameOver = true;
		scores.Add(new HighScoreEntry()
		{
			Score = LastScore
		});
		
		_robots.Clear();
		Inventory.Clear();
	}

	public void AddRobot(Robot robot)
	{
		_robots.Add(robot);
	}

	public void RemoveRobot(Robot robot)
	{
		_robots.Remove(robot);
	}

	public bool HasRobots()
	{
		return _robots.Count > 0;
	}

	public void AddScore(long scoreToAdd)
	{
		lastScore += scoreToAdd;
	}

	public void DamageAllRobots(int damage)
	{
		foreach (Robot robot in _robots)
		{
			robot.GetDamaged(damage);
		}
	}
}