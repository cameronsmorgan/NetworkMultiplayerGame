using UnityEngine;
using Mirror;
using TMPro;
public class Timer : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnTimerChanged))]
    public float timeRemaining = 120f; // 2 minutes, for example

    public TextMeshProUGUI timerText; // Assign in inspector

    private bool isTimerRunning = true;

    void Update()
    {
        if (!isServer) return;

        if (isTimerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTimerRunning = false;
                RpcTimerEnded();
            }
        }
    }

    void OnTimerChanged(float oldTime, float newTime)
    {
        UpdateTimerUI(newTime);
    }

    void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    [ClientRpc]
    void RpcTimerEnded()
    {
        // TODO: Add end-game logic (show results, stop player movement, etc.)
        Debug.Log("Timer ended!");
    }
}
