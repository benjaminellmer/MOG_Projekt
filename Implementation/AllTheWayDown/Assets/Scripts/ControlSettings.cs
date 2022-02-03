using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlSettings
{
    public static InputMethod GetPreferredInputMethod()
    {
        return PlayerPrefs.GetString("inputMethod", "gyro") switch
        {
            "gyro" => InputMethod.Gyroscope,
            "acc" => InputMethod.Accelerometer,
            "touch" => InputMethod.Touch,
            _ => InputMethod.Gyroscope
        };
    }

    public static bool GetMirrorGyro()
    {
        return PlayerPrefs.GetInt("mirrorGyro", 0) == 1;
    }

    public enum InputMethod
    {
        Gyroscope,
        Accelerometer,
        Touch
    }
}