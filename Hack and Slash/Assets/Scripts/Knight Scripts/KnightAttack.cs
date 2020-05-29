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
    public int damage;

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

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<EnemyHPManager>().TakeDamage(damage);
                }
            }

            
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
        
            
    }

    // starts the second attack and damage the enemy if hit
    public void endOfOne()
    {
        if (noOfClicks >= 2)
        {
            animator.SetBool("Attack2", true);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHPManager>().TakeDamage(damage);
            }
        }
        else
        {
            animator.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }

    // starts the third attack if button clicked and damages enemy
    public void endOfTwo()
    {
        if (noOfClicks >= 3)
        {
            animator.SetBool("Attack3", true);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHPManager>().TakeDamage(damage);
            }
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
