using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject BulletPrefab;

    public float moveSpeed = 5.0f; // Adjust this to control the player's movement speed.
    
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("Shoot", 0.5f, 0.5f);  //1s delay, repeat every 1s
    }

    void Update()
    {
        // Get the mouse cursor position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen position to a world position on the same plane as the player
        mousePosition.z = transform.position.z - mainCamera.transform.position.z;
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Move the player towards the mouse cursor
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    }

    private void Shoot(){
        Instantiate(BulletPrefab, transform.position, Quaternion.identity);
    }
}
