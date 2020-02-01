using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreHandler : MonoBehaviour
{


    public void GotoMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
}
