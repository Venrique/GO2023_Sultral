using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 5f;
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

    // Update is called once per frame
    void Update()
    {

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
