using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorScript : MonoBehaviour
{
    public Action<int, int, LevelGenerator.SpawnLocation> onEnter;
    public int roomX;
    public int roomY;
    public LevelGenerator.SpawnLocation spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onEnter(roomX, roomY, spawnLocation);
        }
    }
}
