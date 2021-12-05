using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject[] groundTiles;
    private Vector3 nextSpawnpoint;
    private Quaternion nextSpawnPointRotation;

    public void SpawnTile(int index)
    {
        GameObject temp = Instantiate(groundTiles[index], nextSpawnpoint,  nextSpawnPointRotation);
        nextSpawnPointRotation = temp.transform.GetChild(0).rotation;
        nextSpawnpoint = temp.transform.GetChild(0).transform.position;
    }

    private void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            SpawnTile(0);
            SpawnTile(0);
        }
        
    }
}
