using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset; 
    
    public void Start()
    {
        offset = transform.position - player.transform.position;
    }

    public void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
