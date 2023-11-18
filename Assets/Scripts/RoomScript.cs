using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomScript : MonoBehaviour
{
    [SerializeField] private GameObject wallUpSolid;
    [SerializeField] private GameObject wallDownSolid;
    [SerializeField] private GameObject wallRightSolid;
    [SerializeField] private GameObject wallLeftSolid;

    [SerializeField] private GameObject wallUpDoor;
    [SerializeField] private GameObject wallDownDoor;
    [SerializeField] private GameObject wallRightDoor;
    [SerializeField] private GameObject wallLeftDoor;

    [SerializeField] private GameObject doorUp;
    [SerializeField] private GameObject doorDown;
    [SerializeField] private GameObject doorRight;
    [SerializeField] private GameObject doorLeft;

    [SerializeField] private GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDoors(bool up, bool down, bool right, bool left, int roomX, int roomY, Action<int, int, LevelGenerator.SpawnLocation> onEnter)
    {
        wallUpSolid.SetActive(true);
        wallDownSolid.SetActive(true);
        wallRightSolid.SetActive(true);
        wallLeftSolid.SetActive(true);

        wallUpDoor.SetActive(false);
        wallDownDoor.SetActive(false);
        wallRightDoor.SetActive(false);
        wallLeftDoor.SetActive(false);

        doorUp.SetActive(false);
        doorDown.SetActive(false);
        doorRight.SetActive(false);
        doorLeft.SetActive(false);

        if (up)
        {
            wallUpSolid.SetActive(false);
            wallUpDoor.SetActive(true);
            doorUp.SetActive(true);
            DoorScript door = doorUp.GetComponent<DoorScript>();
            door.onEnter = onEnter;
            door.roomX = roomX;
            door.roomY = roomY - 1;
            door.spawnLocation = LevelGenerator.SpawnLocation.DOWN;
        }
        if (down)
        {
            wallDownSolid.SetActive(false);
            wallDownDoor.SetActive(true);
            doorDown.SetActive(true);
            DoorScript door = doorDown.GetComponent<DoorScript>();
            door.onEnter = onEnter;
            door.roomX = roomX;
            door.roomY = roomY + 1;
            door.spawnLocation = LevelGenerator.SpawnLocation.UP;
        }
        if (right)
        {
            wallRightSolid.SetActive(false);
            wallRightDoor.SetActive(true);
            doorRight.SetActive(true);
            DoorScript door = doorRight.GetComponent<DoorScript>();
            door.onEnter = onEnter;
            door.roomX = roomX + 1;
            door.roomY = roomY;
            door.spawnLocation = LevelGenerator.SpawnLocation.LEFT;
        }
        if (left)
        {
            wallLeftSolid.SetActive(false);
            wallLeftDoor.SetActive(true);
            doorLeft.SetActive(true);
            DoorScript door = doorLeft.GetComponent<DoorScript>();
            door.onEnter = onEnter;
            door.roomX = roomX - 1;
            door.roomY = roomY;
            door.spawnLocation = LevelGenerator.SpawnLocation.RIGHT;
        }
    }

    public void setObstacle(RoomData.Obstacle obstacle)
    {
        switch (obstacle)
        {
            case (RoomData.Obstacle.BLOCKS):
                instantiateObject(block, 3, 1.5f);
                instantiateObject(block, 3, -1.5f);
                instantiateObject(block, -3, 1.5f);
                instantiateObject(block, -3, -1.5f);
                break;
        }
    }

    private void instantiateObject(GameObject gameObject, float x, float y)
    {
        GameObject instance = Instantiate(gameObject);
        instance.transform.position = new Vector3(x, y, 0);
        instance.transform.parent = this.transform;
    }
}
