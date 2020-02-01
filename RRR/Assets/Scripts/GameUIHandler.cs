using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreLabel;
    
    private GameHandler _gameHandler;
    
    private void Awake()
    {
        _gameHandler = FindObjectOfType<GameHandler>();
    }

    private void Update()
    {
        _scoreLabel.text = GameManager.Instance.Score.ToString();
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

    public void AddScore()
    {
        _gameHandler.AddScore(1);
    }
}
