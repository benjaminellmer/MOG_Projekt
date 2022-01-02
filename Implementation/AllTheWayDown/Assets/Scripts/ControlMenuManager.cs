using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlMenuManager : MonoBehaviour
{
    [SerializeField] private Toggle gyroToggle;
    [SerializeField] private Toggle accToggle;
    [SerializeField] private Toggle touchToggle;
    [SerializeField] private Toggle mirrorToggle;

    private void Start()
    {
        string inputMethod = PlayerPrefs.GetString("inputMethod", "gyro");
        accToggle.isOn = false;
        touchToggle.isOn = false;
        mirrorToggle.isOn = PlayerPrefs.GetInt("mirrorGyro", 0) == 1;
        if (inputMethod.Equals("gyro"))
        {
            gyroToggle.isOn = true;
        }
        else if (inputMethod.Equals("acc"))
        {
            accToggle.isOn = true;
        }
        else if (inputMethod.Equals("touch"))
        {
            touchToggle.isOn = true;
        }

        mirrorToggle.onValueChanged.AddListener(MirrorChanged);
        gyroToggle.onValueChanged.AddListener(GyroClicked);
        accToggle.onValueChanged.AddListener(AccClicked);
        touchToggle.onValueChanged.AddListener(TouchClicked);
    }

    private void MirrorChanged(bool val)
    {
        if (val)
        {
            PlayerPrefs.SetInt("mirrorGyro", 1);
        }
        else
        {
            PlayerPrefs.SetInt("mirrorGyro", 0);
        }
    }

    public void GyroClicked(bool val)
    {
        PlayerPrefs.SetString("inputMethod", "gyro");
    }

    public void AccClicked(bool val)
    {
        PlayerPrefs.SetString("inputMethod", "acc");
    }

    public void TouchClicked(bool val)
    {
        PlayerPrefs.SetString("inputMethod", "touch");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}