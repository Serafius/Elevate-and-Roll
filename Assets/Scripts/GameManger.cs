using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

    }

    // Public method to load a level by index
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 1 && levelIndex <= 5) // Ensure the level index is valid
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogError("Invalid level index. Please use an index between 1 and 5.");
        }
    }

    // Method to load the next level
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < 5) // Check if the next level is within bounds
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.Log("You are already on the last level.");
        }
    }

    // Method to reload the current level
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Method to load the main menu (assuming it's at index 0)
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
   

    // Method to quit the game (works only in the built application)
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
