using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync("GameUI", LoadSceneMode.Additive);
    }

    public void AddScore(long scoreToAdd)
    {
        GameManager.Instance.AddScore(scoreToAdd);
    }
}
