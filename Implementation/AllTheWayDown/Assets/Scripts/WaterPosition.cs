using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPosition : MonoBehaviour
{
    public GameObject player;
    private float offset;

    private float y;

    private float x;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = transform.position;
        y = position.y;
        x = position.x;
        offset = position.z - player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float z = player.transform.position.z + offset;
        transform.position = new Vector3(x, y, z);
    }
}
