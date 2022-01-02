using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlSettings
{
    public static InputMethod GetPreferredInputMethod()
    {
        return PlayerPrefs.GetString("inputMethod", "gyro") switch
        {
            "gyro" => InputMethod.GYROSCOPE,
            "acc" => InputMethod.ACCELEROMETER,
            "touch" => InputMethod.TOUCH,
            _ => InputMethod.GYROSCOPE
        };
    }

    public static bool GetMirrorGyro()
    {
        return PlayerPrefs.GetInt("mirrorGyro", 0) == 1;
    }

    public enum InputMethod
    {
        GYROSCOPE,
        ACCELEROMETER,
        TOUCH
    }
}