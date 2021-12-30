using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlSettings
{
    private static InputMethod inputMethod = InputMethod.TOUCH;
    private static bool gyroMirrored = false;
    private static bool accMirrored = false;

    public static InputMethod GetPreferredInputMethod()
    {
        return inputMethod;
    }

    public enum InputMethod
    {
        GYROROSPOCE, ACCELEROMETER, TOUCH
    }
}
