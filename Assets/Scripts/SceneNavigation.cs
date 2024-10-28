using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    // Function to load the MainMenu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Function to load the MainGame scene
    public void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    // Function to close the application
    public void QuitGame()
    {
#if UNITY_EDITOR
        // If running in the Unity editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // If running as a built application, quit
            Application.Quit();
#endif
    }
}
