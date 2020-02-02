using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideHandler : MonoBehaviour
{
    private const string next = "  Next ";
    public TextMeshProUGUI _buttonText;
    private float _buttonAnimationSpeed = .3f;
    private float _buttonSecondsToChange = 0f;

    public SpriteRenderer bgRenderer; 
    
    [SerializeField] private GameObject[] Slides;
    [SerializeField] private float SlideTimeout;

    private int _currentIndex = 0;
    private float _timePassed;

    private void Awake()
    {
        PlayerPrefs.SetInt("story_was_seen_once", 1);
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
        //some go up, some go down...
        _buttonSecondsToChange -= Time.deltaTime;

        if (_buttonSecondsToChange <= 0)
        {
            if (_buttonText.text == next)
                _buttonText.text = next + ">";
            else if (_buttonText.text == next + ">")
                _buttonText.text = next + ">>";
            else
                _buttonText.text = next;

            _buttonSecondsToChange += _buttonAnimationSpeed;
        }

        bgRenderer.color = Color.grey * (1f + Mathf.Sin(Mathf.Rad2Deg * Time.time / 16f) * 0.07f);
    }

    public void NextSlide()
    {
        Slides[_currentIndex].SetActive(false);
            
        _timePassed = 0;
        _currentIndex++;
        if (_currentIndex > Slides.Length - 1)
        {
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
            return;
        }
            
        Slides[_currentIndex].SetActive(true);
    }
}
