using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlSettings
{
    public static InputMethod inputMethod = InputMethod.GYROSCOPE; // TODO: make private
    private static bool gyroMirrored = false;

    public static InputMethod GetPreferredInputMethod()
    {
        return inputMethod;
    }

    public enum InputMethod
    {
        GYROSCOPE,
        ACCELEROMETER,
        TOUCH,
        PC
    }
}