﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
	public void GotoGame()
	{
		SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
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