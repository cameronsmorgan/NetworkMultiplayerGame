using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    [Header("Scene Name")]
    public string firstLevelSceneName = "Level1";  // Replace with your actual Level 1 scene name

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(firstLevelSceneName);  // Regular Unity load
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


