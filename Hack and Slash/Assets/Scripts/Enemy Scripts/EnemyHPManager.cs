using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPManager : MonoBehaviour
{
    // fields
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit");
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    // skeleton dies if hit twice
    public void Death()
    {
        animator.SetFloat("Speed", 0);
        animator.SetTrigger("Dead");
        Debug.Log("Enemy has Died");
    }
}
