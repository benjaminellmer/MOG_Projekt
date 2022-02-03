using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuCamera : MonoBehaviour
{

    Vector3 endPosition;

    public Material skybox;
    // Start is called before the first frame update
    void Start()
    {
        endPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        if (endPosition != currentPosition)
        {
            transform.position = Vector3.MoveTowards(currentPosition, endPosition, 25 * Time.deltaTime);
        }

        float skyBoxValue = Map(currentPosition.x, 0f, 30f, 0f, 0.8f);
        skybox.SetFloat("_CubemapTransition", skyBoxValue);
        DynamicGI.UpdateEnvironment();
    }

    public void MoveBy(int value)
    {
        float newX = endPosition.x + value;
        if (newX >= 0 && newX <= 30)
        {
            endPosition = new Vector3(newX, endPosition.y, endPosition.z);
        }
    }

    float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }
}
