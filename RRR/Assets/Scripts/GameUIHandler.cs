using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _scoreLabel = default;
	[SerializeField] private TextMeshProUGUI _timeLabel = default;
	[SerializeField] private RepairUIHandler _repairPanel = default;

	private void Start()
	{
		_repairPanel.gameObject.SetActive(false);
	}

	private void Update()
	{
		_scoreLabel.text = GameManager.Instance.LastScore.ToString();
		UpdateTimeLabel(GameManager.Instance.remainingTime);
	}

	public void ShowRepairOverlay(Robot robot)
	{
		_repairPanel.Show(robot);
	}
	
	public void HideRepairOverlay()
	{
		_repairPanel.Close();
	}
	
	public void GotoMainMenu()
	{
		GameManager.Instance.GameOver();
		SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
	}

	public void AddScore()
	{
		GameManager.Instance.AddScore(1);
	}

	private void UpdateTimeLabel(float remainingTime)
	{
		int minutes = (int) (remainingTime / 60);
		var seconds = (remainingTime % 60);

		_timeLabel.text = string.Format("{0}:{1:00.0}", minutes, seconds);
	}
}