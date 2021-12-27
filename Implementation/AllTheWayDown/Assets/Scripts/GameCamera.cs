using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Material skybox;
    private float transitionValue;
    private float currentTransitionValue;
    // Start is called before the first frame update
    void Start()
    {
        transitionValue = 0;
        currentTransitionValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionValue > currentTransitionValue)
        {
            currentTransitionValue += 0.002f;
            skybox.SetFloat("_CubemapTransition", currentTransitionValue);
            DynamicGI.UpdateEnvironment();
        }
    }

    public void initiateTransition(int stage)
    {
        float value = getTransitionValue(stage);
        transitionValue = value;
    }

    public void hardTransition(int stage)
    {
        float value = getTransitionValue(stage);
        transitionValue = value;
        currentTransitionValue = value;
        skybox.SetFloat("_CubemapTransition", transitionValue);
        DynamicGI.UpdateEnvironment();
    }

    private float getTransitionValue(int stage)
    {
        switch (stage)
        {
            case 0:
                return 0.0f;
            case 1:
                return 0.8f;
        }

        return 0.0f;
    }
}
