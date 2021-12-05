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
        other.gameObject.transform.rotation = this.gameObject.transform.rotation;
    }

    private void OnTriggerExit(Collider other)
    {
        int index = Random.Range(0, 4);
        groundSpawner.SpawnTile(index);
        Destroy(gameObject, 3f);
    }
}
