using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    // fields
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 2;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                // plays attack 1 animation
                animator.SetBool("Attack1", true);

                // detects enemies
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

                // damage collided enemies
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("Enemy Hit");
                }

                Debug.Log("Attack1");

                
            }

            
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
        
            
    }

    public void endOfOne()
    {
        if (noOfClicks >= 2)
        {
            animator.SetBool("Attack2", true);
        }
        else
        {
            animator.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }

    public void endOfTwo()
    {
        if (noOfClicks >= 3)
        {
            animator.SetBool("Attack3", true);
        }
        else
        {
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }

    public void endOfThree()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        noOfClicks = 0;
        
    }

    // method that draws attack circle
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
