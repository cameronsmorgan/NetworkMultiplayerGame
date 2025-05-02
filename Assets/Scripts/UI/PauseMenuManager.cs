using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject helpPanel;

    [Header("Scene Names")]
    public string homeSceneName = "HomeScene";
    public string nextLevelSceneName = "NextLevel";
    public string firstLevelSceneName = "Level1";

    private NetworkPlayer localPlayer;

    private void Start()
    {
        if (NetworkClient.connection != null && NetworkClient.connection.identity != null)
        {
            localPlayer = NetworkClient.connection.identity.GetComponent<NetworkPlayer>();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        helpPanel.SetActive(false);
    }

    public void OpenHelpPanel()
    {
        pausePanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void BackToPausePanel()
    {
        helpPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void BackToGame()
    {
        Time.timeScale = 1f;
        helpPanel.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        if (localPlayer != null)
        {
            localPlayer.CmdRequestRestartLevel();
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        if (localPlayer != null)
        {
            localPlayer.CmdRequestSceneChange(nextLevelSceneName);
        }
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        if (localPlayer != null)
        {
            localPlayer.CmdRequestSceneChange(homeSceneName);
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        if (localPlayer != null)
        {
            localPlayer.CmdRequestSceneChange(firstLevelSceneName);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


