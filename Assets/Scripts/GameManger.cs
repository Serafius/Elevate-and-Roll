using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance for global access
    public GameObject[] characterListPrefabs; // Array of character prefabs
    public TMP_Text label; // Optional: UI label to display character name

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist GameManager across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnCharacter(); // Spawn the character in the new scene
    }

    public void SpawnCharacter()
    {
        // Find the spawn point dynamically in the scene
        GameObject spawnPointObject = GameObject.Find("PlayerSpawn");
        if (spawnPointObject == null)
        {
            Debug.LogError("Spawn point with name 'PlayerSpawn' not found in the scene!");
            return;
        }

        Transform spawnPoint = spawnPointObject.transform;

        // Get the saved character index from PlayerPrefs
        int selectedCharacter = PlayerPrefs.GetInt("CharacterSelected", 0);

        // Validate the selected character index
        if (selectedCharacter < 0 || selectedCharacter >= characterListPrefabs.Length)
        {
            Debug.LogError("Invalid character index selected!");
            return;
        }

        // Get the prefab for the selected character
        GameObject selectedCharacterPrefab = characterListPrefabs[selectedCharacter];

        // Instantiate the character at the spawn point
        GameObject character = Instantiate(selectedCharacterPrefab, spawnPoint.position, spawnPoint.rotation);

        // Optionally parent the character to the spawn point
        character.transform.SetParent(spawnPoint);

        // Update the UI label with the character's name (if applicable)
        if (label != null)
        {
            label.text = selectedCharacterPrefab.name;
        }

        Debug.Log($"Spawned character: {selectedCharacterPrefab.name} at {spawnPoint.position}");
    }

    // Method to load a level by index
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    // Method to load the next level
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
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

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
