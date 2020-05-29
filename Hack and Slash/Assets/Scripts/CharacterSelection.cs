using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    // fields
    // List of Characters 
    private GameObject[] characterList;
    private int index = 0;

    // start method
    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");

        characterList = new GameObject[transform.childCount];
        
        // puts characters in the array
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        // makes characters invisible
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        // toggle selected character
        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }
    }

    // left arrow click function
    public void ToggleLeft()
    {
        // toggle off current model
        characterList[index].SetActive(false);

        index--;
        // if at beginning of list, go to end of list
        if (index < 0)
        {
            index = characterList.Length - 1;
        }

        // toggle new model one
        characterList[index].SetActive(true);
            
    }

    // right arrow click function
    public void ToggleRight()
    {
        // toggle current model off
        characterList[index].SetActive(false);

        index++;
        // if at beginning of list, go to end of list
        if (index > characterList.Length - 1)
        {
            index = 0;
        }

        // toggle new model one
        characterList[index].SetActive(true);
    }

    // select button function
    public void SelectButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("Game");
    }
}
