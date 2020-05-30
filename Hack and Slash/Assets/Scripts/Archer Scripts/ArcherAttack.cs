using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Serialization;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    // fields
    public Animator anim;
    public Transform crosshair;
    public GameObject arrowPrefab;
    private bool facingRight = true;
    public float speed;


    // Update is called once per frame
    void Update()
    {
        // determining if the player is facing right or left
        if (this.transform.localScale.x > 0)
        {
            facingRight = true;
        }
        else if (this.transform.localScale.x < 0)
        {
            facingRight = false;
        }

        // if left button pressed plays attack animation
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("IsAttacking", true);
        }
    }

    // Animation event : changes attacking bool to false after animation ends
    public void AttackEnd()
    {
        anim.SetBool("IsAttacking", false);
    }

    // Animation event : creates arrow prefab at the certain part of the animation
    public void Attacking()
    {

        // changes the direction of the arrow based on direction of archer
        if (facingRight)
        {
            GameObject arrow = Instantiate(arrowPrefab, crosshair.position, crosshair.rotation);
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0.0f);
        }
        else
        {
            GameObject arrow = Instantiate(arrowPrefab, crosshair.position, Quaternion.identity);
            Vector3 scale = arrow.transform.localScale;
            scale.x *= -1;
            arrow.transform.localScale = scale;
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed, 0.0f);

        }
        
    }


}
