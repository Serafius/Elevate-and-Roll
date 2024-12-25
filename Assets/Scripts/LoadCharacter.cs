using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterListPrefabs;
    public TMP_Text label;

    void Start()
    {
        GameObject spawnPointObject = GameObject.Find("PlayerSpawn");
        if (spawnPointObject == null)
        {
            Debug.LogError("Spawn point with name 'PlayerSpawn' not found in the scene!");
            return;
        }

        Transform spawnPoint = spawnPointObject.transform;

        int selectedCharacter = PlayerPrefs.GetInt("CharacterSelected", 0);

        if (selectedCharacter < 0 || selectedCharacter >= characterListPrefabs.Length)
        {
            Debug.LogError("Invalid character index selected!");
            return;
        }

        GameObject selectedCharacterPrefab = characterListPrefabs[selectedCharacter];

        GameObject character = Instantiate(selectedCharacterPrefab, spawnPoint.position, spawnPoint.rotation);
        character.transform.SetParent(spawnPoint);

        if (label != null)
        {
            label.text = selectedCharacterPrefab.name;
        }

        Debug.Log($"Spawned character: {selectedCharacterPrefab.name} at {spawnPoint.position}");
    }
}
