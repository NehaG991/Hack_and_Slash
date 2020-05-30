using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // fields
    private GameObject[] players;
    private GameObject player;
    private int selectedPlayer;

    private void Start()
    {
        // creates player array and adds the different characters to it
        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("Knight");
        players[1] = GameObject.FindGameObjectWithTag("Archer");

        // gets the current selected character 
        selectedPlayer = PlayerPrefs.GetInt("CharacterSelected");
        //Debug.Log(selectedPlayer);

        // the player field is equal to the current player selected
        if (selectedPlayer == 0)
        {
            player = players[0];
        }

        if (selectedPlayer == 1)
        {
            player = players[1];
        }

    }

    // camera follows current selected character
    private void LateUpdate()
    {
        
        Vector3 temp = transform.position;

        temp.x = player.transform.position.x;
        
        // making sure the x value doesn't go past background bound
        if (temp.x < -1.5)
        {
            temp.x = -1.5f;
        }

        transform.position = temp;

    }


}
