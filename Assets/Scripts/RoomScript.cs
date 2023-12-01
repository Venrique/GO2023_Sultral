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

    [SerializeField] private GameObject doorUpTrigger;
    [SerializeField] private GameObject doorDownTrigger;
    [SerializeField] private GameObject doorRightTrigger;
    [SerializeField] private GameObject doorLeftTrigger;

    [SerializeField] private GameObject doorUp;
    [SerializeField] private GameObject doorDown;
    [SerializeField] private GameObject doorRight;
    [SerializeField] private GameObject doorLeft;

    [SerializeField] private GameObject invisibleWallUp;
    [SerializeField] private GameObject invisibleWallDown;
    [SerializeField] private GameObject invisibleWallRight;
    [SerializeField] private GameObject invisibleWallLeft;

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject exit;

    [SerializeField] private GameObject block;
    [SerializeField] private GameObject wall;

    public void setupRoom(RoomData roomData, Action<int, int, LevelGenerator.SpawnLocation> onEnter)
    {
        setDoors(roomData.doorUp, roomData.doorDown, roomData.doorRight, roomData.doorLeft, roomData.x, roomData.y, onEnter, roomData.complete);
        setObstacle(roomData.obstacle);

        if (roomData.end)
        {
            instantiateObject(exit, 0f, 0f);
        }
        else if (roomData.coin)
        {
            GameObject coinObject = instantiateObject(coin, 0f, 0f);
            Coin coinScript = coinObject.GetComponent<Coin>();
            coinScript.room = roomData;
        }
    }

    public void setRoomComplete(RoomData roomData)
    {
        if (roomData.doorUp)
        {
            doorUp.SetActive(false);
            doorUpTrigger.SetActive(true);
        }
        if (roomData.doorDown)
        {
            doorDown.SetActive(false);
            doorDownTrigger.SetActive(true);
        }
        if (roomData.doorRight)
        {
            doorRight.SetActive(false);
            doorRightTrigger.SetActive(true);
        }
        if (roomData.doorLeft)
        {
            doorLeft.SetActive(false);
            doorLeftTrigger.SetActive(true);
        }

        invisibleWallUp.SetActive(false);
        invisibleWallDown.SetActive(false);
        invisibleWallRight.SetActive(false);
        invisibleWallLeft.SetActive(false);
    }

    private void setDoors(bool up, bool down, bool right, bool left, int roomX, int roomY, Action<int, int, LevelGenerator.SpawnLocation> onEnter, bool complete)
    {
        wallUpSolid.SetActive(true);
        wallDownSolid.SetActive(true);
        wallRightSolid.SetActive(true);
        wallLeftSolid.SetActive(true);

        wallUpDoor.SetActive(false);
        wallDownDoor.SetActive(false);
        wallRightDoor.SetActive(false);
        wallLeftDoor.SetActive(false);

        doorUpTrigger.SetActive(false);
        doorDownTrigger.SetActive(false);
        doorRightTrigger.SetActive(false);
        doorLeftTrigger.SetActive(false);

        doorUp.SetActive(false);
        doorDown.SetActive(false);
        doorRight.SetActive(false);
        doorLeft.SetActive(false);

        invisibleWallUp.SetActive(false);
        invisibleWallDown.SetActive(false);
        invisibleWallRight.SetActive(false);
        invisibleWallLeft.SetActive(false);
        if (!complete)
        {
            invisibleWallUp.SetActive(true);
            invisibleWallDown.SetActive(true);
            invisibleWallRight.SetActive(true);
            invisibleWallLeft.SetActive(true);
        }

        if (up)
        {
            wallUpSolid.SetActive(false);
            wallUpDoor.SetActive(true);
            if (complete)
            {
                doorUp.SetActive(false);
                doorUpTrigger.SetActive(true);
            } else
            {
                doorUp.SetActive(true);
                doorUpTrigger.SetActive(false);
            }

            DoorScript door = doorUpTrigger.GetComponent<DoorScript>();
            door.onEnter = onEnter;
            door.roomX = roomX;
            door.roomY = roomY - 1;
            door.spawnLocation = LevelGenerator.SpawnLocation.DOWN;
        }
        if (down)
        {
            wallDownSolid.SetActive(false);
            wallDownDoor.SetActive(true);
            if (complete)
            {
                doorDown.SetActive(false);
                doorDownTrigger.SetActive(true);
            }
            else
            {
                doorDown.SetActive(true);
                doorDownTrigger.SetActive(false);
            }

            DoorScript door = doorDownTrigger.GetComponent<DoorScript>();
            door.onEnter = onEnter;
            door.roomX = roomX;
            door.roomY = roomY + 1;
            door.spawnLocation = LevelGenerator.SpawnLocation.UP;
        }
        if (right)
        {
            wallRightSolid.SetActive(false);
            wallRightDoor.SetActive(true);
            if (complete)
            {
                doorRight.SetActive(false);
                doorRightTrigger.SetActive(true);
            }
            else
            {
                doorRight.SetActive(true);
                doorRightTrigger.SetActive(false);
            }

            DoorScript door = doorRightTrigger.GetComponent<DoorScript>();
            door.onEnter = onEnter;
            door.roomX = roomX + 1;
            door.roomY = roomY;
            door.spawnLocation = LevelGenerator.SpawnLocation.LEFT;
        }
        if (left)
        {
            wallLeftSolid.SetActive(false);
            wallLeftDoor.SetActive(true);
            if (complete)
            {
                doorLeft.SetActive(false);
                doorLeftTrigger.SetActive(true);
            }
            else
            {
                doorLeft.SetActive(true);
                doorLeftTrigger.SetActive(false);
            }

            DoorScript door = doorLeftTrigger.GetComponent<DoorScript>();
            door.onEnter = onEnter;
            door.roomX = roomX - 1;
            door.roomY = roomY;
            door.spawnLocation = LevelGenerator.SpawnLocation.RIGHT;
        }
    }

    private void setObstacle(RoomData.Obstacle obstacle)
    {
        switch (obstacle)
        {
            case (RoomData.Obstacle.BLOCKS):
                instantiateObject(block, 3, 1.5f);
                instantiateObject(block, 3, -1.5f);
                instantiateObject(block, -3, 1.5f);
                instantiateObject(block, -3, -1.5f);
                break;
            case (RoomData.Obstacle.WALLS):
                instantiateObject(wall, 3.5f, 1.5f);
                instantiateObject(wall, -3.5f, -1.5f);
                break;
        }
    }

    private GameObject instantiateObject(GameObject gameObject, float x, float y)
    {
        GameObject instance = Instantiate(gameObject);
        instance.transform.position = new Vector3(x, y, 0);
        instance.transform.parent = this.transform;
        return instance;
    }
}
