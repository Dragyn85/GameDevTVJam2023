using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The GameObject to instantiate.
    [SerializeField] private GameObject entityToSpawn;
    [SerializeField] private int        numEntitiesToSpawn;

    // An instance of the ScriptableObject defined above.
    public Transform[] spawnPoints;

    // This will be appended to the name of the created entities and increment when each is created.
    int instanceNumber = 1;

    void Start()
    {
        SpawnEntities();
    }

    void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < numEntitiesToSpawn; i++)
        {
            // Creates an instance of the prefab at the current spawn point.
            GameObject currentEntity = Instantiate(entityToSpawn, spawnPoints[currentSpawnPointIndex].position, Quaternion.identity);

            // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
            currentEntity.name = entityToSpawn.name + instanceNumber;

            // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;

            instanceNumber++;
        }
    }
}
