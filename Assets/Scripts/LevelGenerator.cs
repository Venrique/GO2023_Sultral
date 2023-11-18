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
    [SerializeField] private AudioPeer musicPlayer;

    private int size = 10;
    private int roomsTarget = 30;
    private int generatedRooms = 0;
    private RoomData[,] rooms;
    private Queue<RoomData> edgeRooms = new Queue<RoomData>();
    private GameObject currentRoom;

    void Start() {
        RoomData startingRoom = generateLevel();
        Debug.Log(startingRoom.x + " " + startingRoom.y);
        printRooms();
        showRoom(startingRoom.x, startingRoom.y, SpawnLocation.CENTER);
    }

    private RoomData generateLevel()
    {
        generatedRooms = 0;
        rooms = new RoomData[size, size];

        int x = Random.Range(0, size);
        int y = Random.Range(0, size);
        RoomData startingRoom = new RoomData(0, 1.0f);
        addRoom(startingRoom, x, y);
        edgeRooms.Enqueue(startingRoom);

        while (edgeRooms.Count != 0 && generatedRooms < roomsTarget)
        {
            RoomData edgeRoom = edgeRooms.Dequeue();
            int roomX = edgeRoom.x;
            int roomY = edgeRoom.y;

            List<SpawnLocation> roomSpots = new List<SpawnLocation>();
            if (positionAvailable(roomX + 1, roomY))
            {
                roomSpots.Add(SpawnLocation.RIGHT);
            }
            if (positionAvailable(roomX - 1, roomY))
            {
                roomSpots.Add(SpawnLocation.LEFT);
            }
            if (positionAvailable(roomX, roomY + 1))
            {
                roomSpots.Add(SpawnLocation.DOWN);
            }
            if (positionAvailable(roomX, roomY - 1))
            {
                roomSpots.Add(SpawnLocation.UP);
            }

            if (roomSpots.Count > 0)
            {
                int numRooms = Mathf.Min(Random.Range(1, roomSpots.Count + 1), roomsTarget - generatedRooms);
                for (int i = 0; i < numRooms; i++)
                {
                    int spotIndex = Random.Range(0, roomSpots.Count);
                    SpawnLocation randomSpot = roomSpots[spotIndex];
                    RoomData newRoom = new RoomData(edgeRoom.depth + 1, Random.Range(0.5f, 1.5f));
                    int newRoomX = roomX;
                    int newRoomY = roomY;
                    switch (randomSpot)
                    {
                        case SpawnLocation.RIGHT:
                            newRoomX = roomX + 1;
                            edgeRoom.doorRight = true;
                            newRoom.doorLeft = true;
                            break;
                        case SpawnLocation.LEFT:
                            newRoomX = roomX - 1;
                            edgeRoom.doorLeft = true;
                            newRoom.doorRight = true;
                            break;
                        case SpawnLocation.DOWN:
                            newRoomY = roomY + 1;
                            edgeRoom.doorDown = true;
                            newRoom.doorUp = true;
                            break;
                        case SpawnLocation.UP:
                            newRoomY = roomY - 1;
                            edgeRoom.doorUp = true;
                            newRoom.doorDown = true;
                            break;
                    }
                    addRoom(newRoom, newRoomX, newRoomY);
                    edgeRooms.Enqueue(newRoom);
                    roomSpots.Remove(randomSpot);
                }
            }
        }

        return startingRoom;
    }

    private void addRoom(RoomData room, int x, int y)
    {
        rooms[y, x] = room;
        room.x = x;
        room.y = y;
        generatedRooms++;
    }

    private void showRoom(int x, int y, SpawnLocation spawnLocation)
    {
        player.SetActive(false);
        Destroy(currentRoom);

        currentRoom = Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity);
        currentRoom.transform.parent = this.transform;

        RoomScript roomScript = currentRoom.GetComponent<RoomScript>();
        RoomData roomData = rooms[y, x];
        bool roomUp = roomData.doorUp;
        bool roomDown = roomData.doorDown;
        bool roomRight = roomData.doorRight;
        bool roomLeft = roomData.doorLeft;
        roomScript.setDoors(roomUp, roomDown, roomRight, roomLeft, x, y, showRoom);
        roomScript.setObstacle(roomData.obstacle);

        musicPlayer.ChangePitch(roomData.speed);

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

    private bool positionAvailable(int x, int y)
    {
        if (y < 0 || y > size - 1)
        {
            return false;
        }
        if (x < 0 || x > size - 1)
        {
            return false;
        }
        if (rooms[y, x] == null)
        {
            return true;
        }
        return false;
    }

    private void printRooms()
    {
        string map = "";
        for (int y=0; y<size; y++)
        {
            for (int x=0; x<size; x++)
            {
                if (positionAvailable(x, y))
                {
                    map += "_";
                } else
                {
                    map += "x";
                }
                map += " ";
            }
            map += "\n";
        }
        Debug.Log(map);
    }
}
