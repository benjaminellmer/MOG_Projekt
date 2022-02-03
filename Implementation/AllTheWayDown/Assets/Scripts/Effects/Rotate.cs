using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private float speedZ;

    void Update()
    {
        transform.Rotate(CalcRotation(speedX), CalcRotation(speedY), CalcRotation(speedZ));   
    }

    float CalcRotation(float speed)
    {
        return 360 * speed * Time.deltaTime;
    }
}