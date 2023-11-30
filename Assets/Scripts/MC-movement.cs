using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public enum BulletFrequency
    {
        bass1, bass2, bass3, bass4, bass5, bass6, bass7, mids1, mids2, mids3, mids4, mids5, mids6, treble1, treble2, treble3, treble4, treble5, treble6, treble7  
    };

    public BulletFrequency bulletFrequency;

    public float moveSpeed; // Adjust this to control the player's movement speed.

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        // Get the mouse cursor position in screen space
        Vector2 mousePosition = Input.mousePosition;

        // Convert the screen position to a world position on the same plane as the player
        //mousePosition.z = transform.position.z - mainCamera.transform.position.z;
        Vector2 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Move the player towards the mouse cursor
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


        /*
        Vector3 direction = targetPosition - transform.position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float movement = moveSpeed * Time.fixedDeltaTime;
        if (direction.magnitude > 0.05f)
        {
            rb.MovePosition(rb.position + new Vector2(direction.normalized.x, direction.normalized.y) * movement);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        */
    }
}
