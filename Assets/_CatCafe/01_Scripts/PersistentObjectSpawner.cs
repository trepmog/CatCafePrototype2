using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject persistentObjectPrefab = null;

    // PRIVATE STATE
    static bool hasSpawned = false;

    private void Awake()
    {
        if (hasSpawned)
            return;

        if (!hasSpawned && GameObject.FindWithTag("Persistent") == null)
        {
            SpawnPersistentObjects();
            hasSpawned = true;
        }

    }

    private void SpawnPersistentObjects()
    {
        GameObject persistentObject = Instantiate(persistentObjectPrefab);
        DontDestroyOnLoad(persistentObject);
    }
}
