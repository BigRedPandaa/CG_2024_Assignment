using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    private int currentScore = 0;
    private int target = 0;

    private void Start()
    {
        UpdateScoreDisplay();
        target = GameManager.instance.targetScore;
    }

    private void Update()
    {
        currentScore = GameManager.instance.currentScore;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + currentScore.ToString() + "/" + target;
    }
}
