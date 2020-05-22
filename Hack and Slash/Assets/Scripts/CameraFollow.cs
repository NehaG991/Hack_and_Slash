using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // fields
    public GameObject player;

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
