using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Gun variables
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform BulletSpawn1;
    [SerializeField] private Transform BulletSpawn2;
    [SerializeField] private Transform BulletSpawn3;
    [SerializeField] private Transform BulletSpawn4;
    //[Range(0.1f, 1f)]
    //[SerializeField] private float fireRate = 0.5f;

    public Transform playerTransform; // Assign the player object in the Inspector

    public int numberOfBullets = 1; // Adjust this to control the number of bullets fired at once.

    public float moveSpeed = 5.0f; // Adjust this to control the player's movement speed.

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("Shoot", 0.5f, 0.5f);
    }

    void Update()
    {
        // Get the mouse cursor position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen position to a world position on the same plane as the player
        mousePosition.z = playerTransform.position.z - mainCamera.transform.position.z;
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Move the player towards the mouse cursor
        playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

    }

    private void Shoot()
    {
        switch (numberOfBullets)
        {
            case 1:
                Instantiate(BulletPrefab, BulletSpawn1.position, BulletSpawn1.rotation);
                break;
            case 2:
                Instantiate(BulletPrefab, BulletSpawn1.position, BulletSpawn1.rotation);
                Instantiate(BulletPrefab, BulletSpawn2.position, BulletSpawn2.rotation);
                break;
            case 3:
                Instantiate(BulletPrefab, BulletSpawn1.position, BulletSpawn1.rotation);
                Instantiate(BulletPrefab, BulletSpawn2.position, BulletSpawn2.rotation);
                Instantiate(BulletPrefab, BulletSpawn3.position, BulletSpawn3.rotation);
                break;
            case 4:
                Instantiate(BulletPrefab, BulletSpawn1.position, BulletSpawn1.rotation);
                Instantiate(BulletPrefab, BulletSpawn2.position, BulletSpawn2.rotation);
                Instantiate(BulletPrefab, BulletSpawn3.position, BulletSpawn3.rotation);
                Instantiate(BulletPrefab, BulletSpawn4.position, BulletSpawn4.rotation);
                break;
        }
        /*
        Instantiate(BulletPrefab, BulletSpawn1.position, BulletSpawn1.rotation);
        Instantiate(BulletPrefab, BulletSpawn2.position, BulletSpawn2.rotation);
        Instantiate(BulletPrefab, BulletSpawn3.position, BulletSpawn3.rotation);
        Instantiate(BulletPrefab, BulletSpawn4.position, BulletSpawn4.rotation);
        */
    }
    
}
