using System;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
	private bool _storyWasSeen = false;
	
	private void Awake()
	{
		_storyWasSeen = PlayerPrefs.GetInt("story_was_seen_once", 0) == 1;
	}

	private void Start()
	{
		if (!_storyWasSeen)
		{
			GotoStory();
			return;
		}

		var buttons = FindObjectsOfType<Button>();
		foreach (var button in buttons)
		{
			EventTrigger.Entry eventtype = new EventTrigger.Entry();
			eventtype.eventID = EventTriggerType.PointerEnter;
			eventtype.callback.AddListener((eventData) =>
			{
				var textMP = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
				if (!textMP.text.StartsWith("£")) textMP.text = "£" + textMP.text;
			});
			button.gameObject.AddComponent<EventTrigger>().triggers.Add(eventtype);
			
			eventtype = new EventTrigger.Entry();
			eventtype.eventID = EventTriggerType.PointerExit;
			eventtype.callback.AddListener((eventData) =>
			{
				var textMP = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
				if (textMP.text.StartsWith("£")) textMP.text = textMP.text.Substring(1);
			});
			button.gameObject.AddComponent<EventTrigger>().triggers.Add(eventtype);
		}
	}

	private void Update()
	{
		Camera.main.backgroundColor = Color.grey * (1f + Mathf.Sin(Mathf.Rad2Deg * Time.time / 16f) * 0.07f);
	}

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