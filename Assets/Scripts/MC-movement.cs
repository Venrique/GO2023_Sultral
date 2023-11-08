using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Gun variables
    [SerializeField] public GameObject BulletPrefab;

    public enum BulletType
    {
        bass1, bass2, bass3, bass4, bass5, bass6, bass7, mids1, mids2, mids3, mids4, mids5, mids6, treble1, treble2, treble3, treble4, treble5, treble6, treble7  
    };

    public BulletType bulletType;
    public float fireRate = 1f;

    public float moveSpeed = 5.0f; // Adjust this to control the player's movement speed.

    private Camera mainCamera;

    private float lastSpawned;

    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public bool upLeft;
    public bool upRight;
    public bool downLeft;
    public bool downRight;

    void Start()
    {
        mainCamera = Camera.main;
        //InvokeRepeating("Shoot", 0.5f, fireRate);
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



        if (Time.time - lastSpawned > fireRate)
        {
            lastSpawned = Time.time;
            Shoot();
        }

    }

    private void Shoot()
    {

        if (up)
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        }

        if (down)
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 180));
        }

        if (left)
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 90));
        }

        if (right)
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 270));
        }

        if (upLeft)
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 45));
        }

        if (upRight)
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 315));
        }

        if (downLeft)
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 135));
        }

        if (downRight)
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 225));
        }

        /*
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
        */
    }

}
