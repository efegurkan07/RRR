using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private float _remainingTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _remainingTime = GameManager.Instance.TimePerLevel;
        SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);
        
        GameManager.Instance.StartNewGame();
    }

    private void LateUpdate()
    {
        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0)
        {
            OnTimeExpired();
        }
    }

    public void AddScore(long scoreToAdd)
    {
        GameManager.Instance.AddScore(scoreToAdd);
    }

    public float GetRemainingTime()
    {
        return _remainingTime;
    }

    public void OnTimeExpired()
    {
        GameManager.Instance.GameOver();
        SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
    }
}
