using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public enum SpawnLocation
    {
        CENTER, UP, DOWN, RIGHT, LEFT
    };

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject room;

    private int height = 4;
    private int width = 4;
    private int[,] rooms = {
        { 0, 0, 1, 0 },
        { 0, 1, 1, 1 },
        { 1, 1, 1, 0 },
        { 0, 1, 0, 0 }
    };

    private GameObject roomInstance;

    // Start is called before the first frame update
    void Start() {
        showRoom(1, 2, SpawnLocation.CENTER);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void showRoom(int x, int y, SpawnLocation spawnLocation)
    {
        player.SetActive(false);
        Destroy(roomInstance);

        roomInstance = Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity);
        roomInstance.transform.parent = this.transform;

        int roomX = x;
        int roomY = height - y - 1;

        bool roomUp = false;
        if (roomY > 0)
        {
            if (rooms[roomY - 1, roomX] == 1)
            {
                roomUp = true;
            }
        }

        bool roomDown = false;
        if (roomY < height - 1)
        {
            if (rooms[roomY + 1, roomX] == 1)
            {
                roomDown = true;
            }
        }

        bool roomRight = false;
        if (roomX < width - 1)
        {
            if (rooms[roomY, roomX + 1] == 1)
            {
                roomRight = true;
            }
        }

        bool roomLeft = false;
        if (roomX > 0)
        {
            if (rooms[roomY, roomX - 1] == 1)
            {
                roomLeft = true;
            }
        }

        RoomScript roomScript = roomInstance.GetComponent<RoomScript>();
        roomScript.setDoors(roomUp, roomDown, roomRight, roomLeft, x, y, showRoom);

        switch (spawnLocation)
        {
            case SpawnLocation.CENTER:
                player.transform.position = new Vector3(0, 0, 0);
                break;
            case SpawnLocation.UP:
                player.transform.position = new Vector3(0, 2, 0);
                break;
            case SpawnLocation.DOWN:
                player.transform.position = new Vector3(0, -2, 0);
                break;
            case SpawnLocation.RIGHT:
                player.transform.position = new Vector3(4, 0, 0);
                break;
            case SpawnLocation.LEFT:
                player.transform.position = new Vector3(-4, 0, 0);
                break;
        }

        player.SetActive(true);
    }
}
