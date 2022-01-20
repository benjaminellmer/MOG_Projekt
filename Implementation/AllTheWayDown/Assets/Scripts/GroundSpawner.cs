using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] stage0;
    [SerializeField] private GameObject[] stage1;
    [SerializeField] private GameObject[] stage2;
    public GameObject[][] groundTiles;
    private Vector3 nextSpawnpoint;
    private Quaternion nextSpawnPointRotation;
    public int nextIndex;

    public void SpawnTile(int stage, int index)
    {
        if (stage < groundTiles.Length)
        {
            if (index < groundTiles[stage].Length)
            {
                GameObject temp = Instantiate(groundTiles[stage][index], nextSpawnpoint, nextSpawnPointRotation);
                nextIndex = index;
                nextSpawnPointRotation = temp.transform.GetChild(0).rotation;
                nextSpawnpoint = temp.transform.GetChild(0).transform.position;
            }
            else
            {
                if (index != 0) SpawnTile(stage, --index);
                else SpawnTile(--stage, index);
            }
        }
        else if (stage != 0) SpawnTile(--stage, index);
    }

    private void Start()
    {
        var stage = GameManager.inst.getStage();
        groundTiles = new GameObject[3][];
        groundTiles[0] = stage0;
        groundTiles[1] = stage1;
        groundTiles[2] = stage2;
        // index out of bounds are caught in SpawnTile
        SpawnTile(stage, 0);
        SpawnTile(stage, 1);
        SpawnTile(stage, 2);
    }
}