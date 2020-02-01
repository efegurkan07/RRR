using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
	private bool _storyWasSeen = false;
	
	private void Awake()
	{
		_storyWasSeen = PlayerPrefs.GetInt("story_was_seen", 0) == 1;
	}

	public void GotoGame()
	{
		if (!_storyWasSeen)
		{
			GameManager.Instance.CurrentState = GameState.FirstTimeStory;
			GotoStory();
		}
		else
		{
			SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
		}
	}

	public void GotoCredits()
	{
		SceneManager.LoadSceneAsync("Credits", LoadSceneMode.Single);
	}

	public void GotoHighscore()
	{
		SceneManager.LoadSceneAsync("Highscore", LoadSceneMode.Single);
	}
	
	public void GotoStory()
	{
		SceneManager.LoadSceneAsync("Story", LoadSceneMode.Single);
	}
}