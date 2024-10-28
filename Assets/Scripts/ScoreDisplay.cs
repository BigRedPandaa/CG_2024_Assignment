using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign your TMP text component in the Inspector
    private int currentScore = 0;

    private void Start()
    {
        UpdateScoreDisplay();
    }

    // Call this method to increase the score
    private void Update()
    {
        currentScore = GameManager.instance.currentScore;
        UpdateScoreDisplay();
    }

    // Method to update the score display
    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
