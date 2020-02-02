using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreHandler : MonoBehaviour
{

	[SerializeField] private GameObject HighscoreList = default;
	[SerializeField] private ScoreListItem ScoreListItem = default;

	private void Start()
	{
		var highscoreEntries = GameManager.Instance.scores
			.GroupBy(x => x.Score)
			.Select(group => group.First())
			.OrderByDescending(x => x.Score)
			.Take(5);
		
		foreach (var score in highscoreEntries)
		{
			var listItem = Instantiate(ScoreListItem, HighscoreList.transform);
			listItem.SetScore(score.Score);
		}
	}

	public void GotoMainMenu()
	{
		SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
	}
}
