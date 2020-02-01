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
		foreach (var score in GameManager.Instance.scores.OrderByDescending(x => x.Score))
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