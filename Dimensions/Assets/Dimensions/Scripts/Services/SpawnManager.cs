using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The GameObject to instantiate.
    [SerializeField] private GameObject entityToSpawn;
    //[SerializeField] private int        numEntitiesToSpawn;
    [SerializeField] private float      yOffset = 1;

    // An instance of the ScriptableObject defined above.
    [SerializeField] internal SpawnPoint[] spawnPoints;

    // This will be appended to the name of the created entities and increment when each is created.
    int instanceNumber = 1;

    void Start()
    {
        FindSpawnPoints();
        SpawnEntities();
    }

    private void FindSpawnPoints()
    {
        spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
    }


    void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;

        // for (int i = 0; i < numEntitiesToSpawn; i++)
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var spawnPos = spawnPoints[currentSpawnPointIndex].transform.position;
            spawnPos.y += yOffset;

            GameObject currentEntity = Instantiate(entityToSpawn, spawnPos, Quaternion.identity);
            currentEntity.name = entityToSpawn.name + instanceNumber;
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;

            instanceNumber++;
        }
    }
}
