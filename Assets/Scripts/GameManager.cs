using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int targetScore = 30;
    public int currentScore = 0;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore()
    {
        Debug.Log("score");
        currentScore += 1;

        if (currentScore >= targetScore)
        {
            LoadVictoryScene();
        }
    }

    private void LoadVictoryScene()
    {
        SceneManager.LoadScene("Victory");
        ResetScore();
    }


    public void ResetScore()
    {
        currentScore = 0;
    }
}
