using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // fields
    private int damage = 50;

    // destroys object also long it doesn't collide with a patrol constaint object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "PatrolConstraints")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHPManager>().TakeDamage(damage);
            }
            Debug.Log(collision.name);
            Destroy(gameObject);
        }

    }
}
