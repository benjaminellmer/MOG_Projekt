using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ControlMenuManager : MonoBehaviour
{
    public void GyroClicked()
    {
        PlayerPrefs.SetString("inputMethod", "gyro");
    }
    
    public void AccClicked()
    {
        PlayerPrefs.SetString("inputMethod", "acc");
    }
    
    public void TouchClicked()
    {
        PlayerPrefs.SetString("inputMethod", "touch");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
