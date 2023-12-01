using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class EnemyPoints
{
    public int startPoint { get; set; }
    public int endPoint { get; set; }
}

public class SpawnerScript : MonoBehaviour
{
    private int frequencyCounter = 0;
    public GameObject enemy;
    public int maxNumberEnemies;
    public float secondsToRespawnEnemy;

    //private int enemyCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(ExampleCoroutine());
        /* if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        } */
    }

    IEnumerator SpawnEnemies()
    {
        Queue<EnemyPoints> enemiesQueue = CreateEnemiesQueue(maxNumberEnemies);

        foreach (var enemyPoints in enemiesQueue)
        {
            GameObject newEnemy = SpawnEnemy();
            Path enemyScript = newEnemy.GetComponent<Path>();
            enemyScript.points = SetSpawnPoints(enemyPoints.startPoint, enemyPoints.endPoint);
            yield return new WaitForSeconds(secondsToRespawnEnemy);
        }
    }

    GameObject SpawnEnemy()
    {
        //Vector3 offset = new Vector3(x, y, z);
        GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        newEnemy.GetComponent<ShotTimeTrigger>().value = frequencyCounter % 8;
        frequencyCounter++;
        newEnemy.GetComponent<ShotTimeTrigger>().offBeat = false;

        return newEnemy;
    }

    GameObject[] SetSpawnPoints(int startPoint, int endPoint)
    {
        var newEndpoint = endPoint;
        var newStartPoint = startPoint;
        if (endPoint == 4)
        {
            newEndpoint = 0;
        }

        if (startPoint == 4)
        {
            newStartPoint = 3;
            newEndpoint = 0;
        }
        List<GameObject> list = new List<GameObject>();
        list.Add(GameObject.Find("Point" + newStartPoint));
        list.Add(GameObject.Find("Point" + newEndpoint ));
        return list.ToArray();
    }

    public static Queue<EnemyPoints> CreateEnemiesQueue(int maxEnemiesNumber)
    {
        Queue<EnemyPoints> cola = new Queue<EnemyPoints>();

        // Calcular la cantidad de enemigos por cuadrante
        int enemigosPorCuadrante = maxEnemiesNumber / 4;

        // Distribuir enemigos en cada cuadrante
        for (int i = 0; i < maxEnemiesNumber; i++)
        {
            int startPoint = i / enemigosPorCuadrante;
            int endPoint = startPoint + 1;

            // Crear un objeto EnemyInfo con propiedades de inicio y fin
            EnemyPoints enemyInfo = new EnemyPoints
            {
                startPoint = startPoint,
                endPoint = endPoint
            };

            // Agregar el objeto a la cola
            cola.Enqueue(enemyInfo);
        }

        return cola;
    }
}


