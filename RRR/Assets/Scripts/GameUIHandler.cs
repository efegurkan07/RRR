using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _scoreLabel = default;
	[SerializeField] private RepairUIHandler _repairPanel = default;

	private void Start()
	{
		_repairPanel.gameObject.SetActive(false);
	}

	private void Update()
	{
		_scoreLabel.text = GameManager.Instance.LastScore.ToString();
	}

	public void ShowRepairOverlay(Robot robot)
	{
		_repairPanel.Show(robot);
	}
	
	public void HideRepairOverlay()
	{
		_repairPanel.Close();
	}

	public void LaunchJetpack()
	{
		FindObjectOfType<GameHandler>().LaunchJetpack();
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
}