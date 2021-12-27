using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject[] stage0;
    public GameObject[] stage1;
    public GameObject[][] groundTiles;
    private Vector3 nextSpawnpoint;
    private Quaternion nextSpawnPointRotation;
    public int nextIndex;

    public void SpawnTile(int stage, int index)
    {
        GameObject temp = Instantiate(groundTiles[stage][index], nextSpawnpoint,  nextSpawnPointRotation);
        nextIndex = index;
        nextSpawnPointRotation = temp.transform.GetChild(0).rotation;
        nextSpawnpoint = temp.transform.GetChild(0).transform.position;
    }

    private void Start()
    {
        var stage = GameManager.inst.getStage();
        groundTiles = new GameObject[2][];
        groundTiles[0] = stage0;
        groundTiles[1] = stage1;
        SpawnTile(stage, 0);
        SpawnTile(stage, 1);
        SpawnTile(stage, 2);
    }
}

