using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner groundSpawner;
    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.rotation = gameObject.transform.rotation;
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.inst.IncTiles();
        var stage = GameManager.inst.GetStage();
        int index;
        do
        {
            index = Random.Range(0, groundSpawner.groundTiles[stage].Length);
        } while (index == groundSpawner.nextIndex);
        groundSpawner.SpawnTile(stage, index);
        Destroy(gameObject, 5f);
    }
}
