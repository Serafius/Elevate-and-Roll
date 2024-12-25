using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterList;
    public int selectedCharacter = 0;  

    public void NextCharacter()
    {
        characterList[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characterList.Length;
        characterList[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characterList[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characterList.Length;
        }
        characterList[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("CharacterSelected", selectedCharacter);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
