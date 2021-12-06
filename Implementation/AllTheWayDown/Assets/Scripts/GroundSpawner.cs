using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject[] groundTiles;
    private Vector3 nextSpawnpoint;
    private Quaternion nextSpawnPointRotation;
    public int nextIndex;

    public void SpawnTile(int index)
    {
        GameObject temp = Instantiate(groundTiles[index], nextSpawnpoint,  nextSpawnPointRotation);
        nextIndex = index;
        nextSpawnPointRotation = temp.transform.GetChild(0).rotation;
        nextSpawnpoint = temp.transform.GetChild(0).transform.position;
    }

    private void Start()
    {
        SpawnTile(0);
        SpawnTile(1);
        SpawnTile(3);
    }
}
