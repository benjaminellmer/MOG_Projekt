using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Material skybox;
    private float transitionValue;
    private float currentTransitionValue;
    
    void Start()
    {
        transitionValue = 0;
        currentTransitionValue = 0;
    }

    void Update()
    {
        if (transitionValue > currentTransitionValue)
        {
            currentTransitionValue += 0.002f;
            skybox.SetFloat("_CubemapTransition", currentTransitionValue);
            DynamicGI.UpdateEnvironment();
        }
    }

    public void InitiateTransition(int stage)
    {
        float value = GetTransitionValue(stage);
        transitionValue = value;
    }

    public void HardTransition(int stage)
    {
        float value = GetTransitionValue(stage);
        transitionValue = value;
        currentTransitionValue = value;
        skybox.SetFloat("_CubemapTransition", transitionValue);
        DynamicGI.UpdateEnvironment();
    }

    private float GetTransitionValue(int stage)
    {
        switch (stage)
        {
            case 0:
                return 0.0f;
            case 1:
                return 0.0f;
            case 2:
                return 0.8f;
        }
        return 0.0f;
    }
}
