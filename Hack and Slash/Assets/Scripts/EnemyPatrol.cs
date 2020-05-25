using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // fields
    public float speed;
    public bool moveRight;
    public SpriteRenderer enemy;
    public Animator ani;
    bool onPatrol;

    private void Awake()
    {
        onPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {

        // moves enemy right 
        if (moveRight && onPatrol)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            enemy.flipX = false;
            
        }

        // moves enemy left
        else if (moveRight == false && onPatrol)
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            enemy.flipX = true;
        }

        // plays either walk or idle animation based on speed
        ani.SetFloat("Speed", speed);
    }

    // checks if at the max side of patrol and flips the enemy
    // if collides with player attack trigger, stops and attacks
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // changes direction if still in patrol
        if (collision.gameObject.CompareTag("PatrolConstraints") && onPatrol)
        {
            if (moveRight)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
        }

        else if (collision.gameObject.CompareTag("AttackTrigger"))
        {
            onPatrol = false;
            ani.SetFloat("Speed", 0);
            ani.SetBool("Attack1", true);
        }
    }

    // Enemy Continues patrol if player is out of range
    void AfterAttack()
    {
        onPatrol = true;
        ani.SetFloat("Speed", speed);
        ani.SetBool("Attack1", false);
    }
}
