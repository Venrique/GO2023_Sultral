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
    [SerializeField] private GameObject musicPlayer;
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject purpleEnemySpawner;
    [SerializeField] private GameObject orangeEnemySpawner;
    [SerializeField] private GameObject blueEnemySpawner;
    [SerializeField] private GameObject redEnemySpawner;

    public static readonly int RATIO_CHANCE_A = 10;
    public static readonly int RATIO_CHANCE_B = 30;

    public static readonly int RATIO_CHANCE_C = 30;
    //                         ...
    public static readonly int RATIO_CHANCE_N = 30;

    public static readonly int RATIO_TOTAL = RATIO_CHANCE_A
                                        + RATIO_CHANCE_B
                                        + RATIO_CHANCE_C
                                            // ...
                                        + RATIO_CHANCE_N;
    [SerializeField] public GameObject FireRateBuffPrefab;
    [SerializeField] public GameObject SpeedUpBuffPrefab;
    [SerializeField] public GameObject BulletsBuffPrefab;
    [SerializeField] public GameObject RegenBuffPrefab;

    private int size = 10;
    private int roomsTarget = 30;
    private int generatedRooms = 0;
    private RoomData[,] rooms;
    private Queue<RoomData> edgeRooms = new Queue<RoomData>();
    private GameObject currentRoom;
    private RoomData currentRoomData;
    private int requiredEnemyKills = 0;
    private List<GameObject> spawners = new List<GameObject>();

    void Start() {
        GameData.enemyKills = 0;
        GameData.coins = 0;
        AudioSource audioSource = musicPlayer.GetComponent<AudioSource>();
        switch (GameData.level)
        {
            case 0:
            case 1:
                audioSource.clip = Resources.Load("Sounds/Moonwalk") as AudioClip;
                AudioPeer.sensitivity = 0.28f;
                break;
            case 2:
                audioSource.clip = Resources.Load("Sounds/Factory") as AudioClip;
                AudioPeer.sensitivity = 0.22f;
                break;
            case 3:
                audioSource.clip = Resources.Load("Sounds/Citadel") as AudioClip;
                AudioPeer.sensitivity = 0.30f;
                break;
            case 4:
                audioSource.clip = Resources.Load("Sounds/Moonwalk") as AudioClip;
                AudioPeer.sensitivity = 0.20f;
                break;
            case 5:
                audioSource.clip = Resources.Load("Sounds/Factory") as AudioClip;
                AudioPeer.sensitivity = 0.17f;
                break;
            case 6:
                audioSource.clip = Resources.Load("Sounds/Citadel") as AudioClip;
                AudioPeer.sensitivity = 0.25f;
                break;
        }
        audioSource.Play();

        RoomData startingRoom = generateLevel();
        printRooms();
        showRoom(startingRoom.x, startingRoom.y, SpawnLocation.CENTER);
    }

    void Update()
    {
        if (GameData.enemyKills >= requiredEnemyKills && !currentRoomData.complete)
        {
            GameData.enemyKills = 0;
            RoomScript roomScript = currentRoom.GetComponent<RoomScript>();
            currentRoomData.complete = true;
            roomScript.setRoomComplete(currentRoomData);

            System.Random random = new System.Random();
            int x = random.Next(0, RATIO_TOTAL);

            if ((x -= RATIO_CHANCE_A) < 0) // Test for A
            {
                Instantiate(BulletsBuffPrefab, transform.position, Quaternion.identity);
            }
            else if ((x -= RATIO_CHANCE_B) < 0) // Test for B
            {
                Instantiate(FireRateBuffPrefab, transform.position, Quaternion.identity);
            }
            else if ((x -= RATIO_CHANCE_C) < 0) // Test for C
            { 
                Instantiate(RegenBuffPrefab, transform.position, Quaternion.identity);
            }
            // ... etc
            else // No need for final if statement
            {
                Instantiate(SpeedUpBuffPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private RoomData generateLevel()
    {
        //StartCoroutine(WaitMovement(2));
        generatedRooms = 0;
        rooms = new RoomData[size, size];

        int x = Random.Range(0, size);
        int y = Random.Range(0, size);
        RoomData startingRoom = new RoomData(0, 1.0f);
        startingRoom.setStartRoom();
        startingRoom.obstacle = RoomData.Obstacle.WALLS;
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
                    RoomData newRoom = new RoomData(edgeRoom.depth + 1, Random.Range(0.5f, 1.5f));

                    SpawnLocation randomSpot = roomSpots[Random.Range(0, roomSpots.Count)];
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

                    RoomData.Obstacle obstacle = RoomData.Obstacle.NONE;
                    int randomObstacle = Random.Range(1, 101);
                    if (randomObstacle > 50)
                    {
                        obstacle = RoomData.Obstacle.BLOCKS;
                    } else if (randomObstacle > 75)
                    {
                        obstacle = RoomData.Obstacle.WALLS;
                    }
                    newRoom.obstacle = obstacle;

                    addRoom(newRoom, newRoomX, newRoomY);
                    edgeRooms.Enqueue(newRoom);
                    roomSpots.Remove(randomSpot);
                }
            }
        }

        List<RoomData> sortedRooms = sortRoomsByDepth();
        RoomData endingRoom = sortedRooms[0];
        endingRoom.setEndRoom();

        sortedRooms[1].setCoinRoom();
        sortedRooms[2].setCoinRoom();
        sortedRooms[3].setCoinRoom();

        return startingRoom;
    }

    public List<RoomData> sortRoomsByDepth()
    {
        List<RoomData> roomList = new List<RoomData>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                RoomData room = rooms[i, j];
                if (room != null)
                {
                    roomList.Add(room);
                }
            }
        }
        roomList.Sort((room1, room2) => {
            if (room1.depth == room2.depth)
            {
                return 0;
            }
            else if (room1.depth > room2.depth)
            {
                return -1;
            }
            return 1;
        });
        return roomList;
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
        float wait = 0.5f;
        StartCoroutine(WaitMovement(wait));
        
        
        player.SetActive(false);
        foreach (GameObject spawner in spawners)
        {
            Destroy(spawner);
        }
        spawners = new List<GameObject>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject enemyBullet in enemyBullets)
        {
            Destroy(enemyBullet);
        }

        Destroy(currentRoom);

        currentRoom = Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity);
        currentRoom.transform.parent = this.transform;

        currentRoomData = rooms[y, x];
        RoomScript roomScript = currentRoom.GetComponent<RoomScript>();
        roomScript.setupRoom(currentRoomData, showRoom);

        AudioPeer audioPeer = musicPlayer.GetComponent<AudioPeer>();
        audioPeer.ChangePitch(currentRoomData.speed);

        GameData.enemyKills = 0;
        requiredEnemyKills = 0;
        if (!currentRoomData.complete)
        {
            GameObject PurpleSpawner = Instantiate(purpleEnemySpawner);
            GameObject OrangeSpawner = Instantiate(orangeEnemySpawner);
            GameObject BlueSpawner = Instantiate(blueEnemySpawner);
            GameObject RedSpawner = Instantiate(redEnemySpawner);
            spawners.Add(PurpleSpawner);
            spawners.Add(OrangeSpawner);
            spawners.Add(BlueSpawner);
            spawners.Add(RedSpawner);
            requiredEnemyKills += PurpleSpawner.GetComponent<SpawnerScript>().maxNumberEnemies;
            requiredEnemyKills += OrangeSpawner.GetComponent<SpawnerScript>().maxNumberEnemies;
            requiredEnemyKills += BlueSpawner.GetComponent<SpawnerScript>().maxNumberEnemies;
            requiredEnemyKills += RedSpawner.GetComponent<SpawnerScript>().maxNumberEnemies;
        }
        
        /* switch (spawnLocation)
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
        } */
        player.transform.position = new Vector3(0, 0, 0);
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
                RoomData room = rooms[y, x];
                if (room == null)
                {
                    map += "_";
                } else
                {
                    if (room.start)
                    {
                        map += "S";
                    }
                    else if (room.end)
                    {
                        map += "E";
                    }
                    else if (room.coin)
                    {
                        map += "C";
                    }
                    else
                    {
                        map += "x";
                    }
                }
                map += " ";
            }
            map += "\n";
        }
        Debug.Log(map);
    }


    IEnumerator WaitMovement(float secondsToWait)
    {
        float currentPlayerSpeed = player.GetComponent<Movement>().moveSpeed;
        player.GetComponent<Movement>().moveSpeed = 0;
        player.transform.position = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(secondsToWait);
        player.GetComponent<Movement>().moveSpeed = currentPlayerSpeed;

    
    }
}
