using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField] public GameObject Bullet2t_0Prefab;

    public enum BulletType
    {
        type1, type2, type3, type4
    };

    public BulletType bulletType;
    
    public float fireRate;

    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public bool upLeft;
    public bool upRight;
    public bool downLeft;
    public bool downRight;

    private float lastSpawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - lastSpawned > fireRate)
        {
            lastSpawned = Time.time;
            Shoot();
        }
        
    }

    public void Shoot()
    {

        if (up)
        {
            if(bulletType == BulletType.type1){
                Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            }

            if(bulletType == BulletType.type2){
                Instantiate(Bullet2t_0Prefab, transform.position, Quaternion.Euler(0, 0, 0));
            }
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
