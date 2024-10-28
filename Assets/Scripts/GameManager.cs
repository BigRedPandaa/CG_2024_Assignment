using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // The target score to win
    private int targetScore = 10;
    private int currentScore = 0;

    // Singleton pattern to easily access GameManager from other scripts
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps GameManager persistent between scenes
        }
        else
        {
            Destroy(gameObject); // Ensures there's only one GameManager
        }
    }

    // Method to increase the score
    public void AddScore()
    {
        currentScore+= 5;

        if (currentScore >= targetScore)
        {
            LoadVictoryScene();
        }
    }

    // Method to load the victory scene
    private void LoadVictoryScene()
    {
        SceneManager.LoadScene("Victory");
    }

    // Optional: Reset score if needed
    public void ResetScore()
    {
        currentScore = 0;
    }
}
