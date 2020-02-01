using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreHandler : MonoBehaviour
{

	[SerializeField] private GameObject HighscoreList;
	[SerializeField] private ScoreListItem ScoreListItem;

	private void Start()
	{
		foreach (var score in GameManager.Instance.scores)
		{
			var listItem = Instantiate(ScoreListItem, HighscoreList.transform);
			listItem.SetScore(score);
		}
	}

	public void GotoMainMenu()
	{
		SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
	}
}