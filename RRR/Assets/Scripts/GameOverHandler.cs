using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lastScoreLabel;

    private void Start()
    {
        _lastScoreLabel.text = GameManager.Instance.LastScore.ToString();
    }
    
    public void GotoMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
}
