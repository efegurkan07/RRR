using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] Slides;
    [SerializeField] private float SlideTimeout;

    private int _currentIndex = 0;
    private float _timePassed;

    private void Awake()
    {
        PlayerPrefs.SetInt("story_was_seen", 1);
    }

    private void Start()
    {
        Slides[_currentIndex].SetActive(true);
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed >= SlideTimeout)
        {
            NextSlide();
        }
    }

    public void NextSlide()
    {
        Slides[_currentIndex].SetActive(false);
            
        _timePassed = 0;
        _currentIndex++;
        if (_currentIndex > Slides.Length - 1)
        {
            if (GameManager.Instance.CurrentState == GameState.FirstTimeStory)
            {
                SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
            }

            return;
        }
            
        Slides[_currentIndex].SetActive(true);
    }
}
