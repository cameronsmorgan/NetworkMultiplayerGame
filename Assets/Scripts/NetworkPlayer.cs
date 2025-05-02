using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class NetworkPlayer : NetworkBehaviour
{
    [Command]
    public void CmdRequestRestartLevel()
    {
        if (!isServer) return;

        string currentScene = SceneManager.GetActiveScene().name;
        NetworkManager.singleton.ServerChangeScene(currentScene);
    }

    [Command]
    public void CmdRequestSceneChange(string sceneName)
    {
        if (!isServer) return;

        NetworkManager.singleton.ServerChangeScene(sceneName);
    }
}