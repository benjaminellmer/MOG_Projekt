using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationInterface : MonoBehaviour
{
    [SerializeField] private Button mirrorButton;
    [SerializeField] private Button calibrateYButton;
    
    void Start()
    {
        mirrorButton.onClick.AddListener(MirrorControls);
        calibrateYButton.onClick.AddListener(CalibrateYAxis);
    }

    void MirrorControls()
    {
        Debug.Log("Mirroring!");
        Calibration.gyroMirrorControls = !Calibration.gyroMirrorControls;
    }
    
    void CalibrateYAxis()
        {
        Debug.Log("Calibrating!");
            // Movement controls using the Gyroscope of the Phone
            // retrieved from: https://elearning.fh-ooe.at/pluginfile.php/486729/mod_resource/content/0/07_controls_notes.pdf slide 35
            Input.gyro.enabled = true;
    
            var yawPitchRoll = Input.gyro.attitude.eulerAngles;
    
            // read angle in range [0, 360]
            var rawValueY = yawPitchRoll.y;
    
            // convert values to [-180,180]
            if (rawValueY > 180) rawValueY -= 360;
            if (rawValueY < -180) rawValueY += 360;
    
            // scale to angle
            rawValueY = Mathf.Clamp(rawValueY / 90, -1, 1);
            Calibration.gyroOffsetY = rawValueY;
        }
}
