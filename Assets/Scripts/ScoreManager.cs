using UnityEngine;
using Mirror;
using TMPro;
public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager Instance;

    [SyncVar(hook = nameof(OnScoreChanged))]
    public int currentScore = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    [Server]
    public void AddPoints(int points)
    {
        currentScore += points;
    }

    void OnScoreChanged(int oldScore, int newScore)
    {
        ScoreUI.Instance?.UpdateScoreDisplay(newScore);
    }
}
