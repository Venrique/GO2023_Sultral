using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] private float speed = 17f;
    [SerializeField] public bool isEnemyBullet;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy (gameObject);
        }

        if (other.gameObject.tag == "Enemy" && !isEnemyBullet)
        {
            Health enemyHealth = other.gameObject.GetComponent<Health>();
            enemyHealth.TakeDamage(50);
        }

        if (other.gameObject.tag == "Player" && isEnemyBullet)
        {
            Health enemyHealth = other.gameObject.GetComponent<Health>();
            enemyHealth.TakeDamage(100);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
