using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject helpPanel;

    [Header("Scene Names")]
    public string homeSceneName = "HomeScene";         // Change to your home/start screen
    public string nextLevelSceneName = "NextLevel";    // Change to your next level
    public string firstLevelSceneName = "Level1";      // Change to your first level

    // Pause the game and show pause panel
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    // Resume game from pause panel
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        helpPanel.SetActive(false);
    }

    // From pause to help
    public void OpenHelpPanel()
    {
        pausePanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    // From help back to pause panel
    public void BackToPausePanel()
    {
        helpPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    // From help back to gameplay
    public void BackToGame()
    {
        Time.timeScale = 1f;
        helpPanel.SetActive(false);
    }

    // Restart current level
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
        }
    }

    // Load next level
    public void NextLevel()
    {
        Time.timeScale = 1f;
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene(nextLevelSceneName);
        }
    }

    // Return to home/start screen
    public void GoHome()
    {
        Time.timeScale = 1f;
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene(homeSceneName);
        }
    }

    // Play button on Start Screen to load first level
    public void PlayGame()
    {
        Time.timeScale = 1f;
        if (NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene(firstLevelSceneName);
        }
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}


