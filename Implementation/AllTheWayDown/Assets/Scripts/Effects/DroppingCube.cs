using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingCube : MonoBehaviour
{
    private bool dropping;
    private float dropSpeed = 5f;

    private void Update()
    {
        if (dropping) transform.Translate(Vector3.down * Time.deltaTime * dropSpeed, Space.World);
        if(transform.position.y < -10) gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) dropping = true;
    }
}