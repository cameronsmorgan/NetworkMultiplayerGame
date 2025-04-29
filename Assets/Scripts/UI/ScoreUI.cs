using UnityEngine;
using TMPro;
public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance;

    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void UpdateScoreDisplay(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + newScore;
        }
    }
}
