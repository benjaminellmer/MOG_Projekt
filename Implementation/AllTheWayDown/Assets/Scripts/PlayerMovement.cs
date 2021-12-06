using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody player;
    [SerializeField] private float directionSpeed = 50f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float sensibility = 30;

    // set to true, if the movement using <- -> and a d should be available
    [SerializeField] private bool debugOnPC = false;

    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (debugOnPC)
        {
            HandleMovementWithKeyboard();
        }
        else
        {
            HandleMovementWithGyroscope();
        }
    }

    void HandleMovementWithKeyboard()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        player.velocity = new Vector3(horizontalInput * directionSpeed, player.velocity.y, forwardSpeed);

        // TODO: Check if the player is already jumping, should only be possible to jump, if the player touches the collider of the ground
        if (Input.GetButtonDown("Jump"))
        {
            player.velocity = new Vector3(horizontalInput * directionSpeed, jumpForce, forwardSpeed);
        }
    }

    void HandleMovementWithGyroscope()
    {
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

        rawValueY = applySensibility(rawValueY);

        Debug.Log(Input.touchCount);


        Vector3 upsideMove = transform.up * 0;
        // Detect Jump
        if (Input.touchCount > 0)
        {
            upsideMove = transform.up * jumpForce * Time.fixedDeltaTime;
        }

        // move body
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;
        Vector3 sideMove = transform.right * rawValueY * directionSpeed * Time.fixedDeltaTime;
        player.MovePosition(transform.position + forwardMove + sideMove + upsideMove);
    }

    /**
     * The sensibility defines the value at, which the whole directional speed should be applied.
     * If the sensibility is 30 the whole directional speed is applied if the phone is tilted 30 degrees.
     */
    private float applySensibility(float value)
    {
        // to avoid DivideByZero
        if (value == 0) return 0;

        // The value should already be 1 at 30 degree rotation, therefore mutliply and then modify it again
        // 180deg = 1, per deg 0.005, 30 deg = 0.16, 0.16 should already be 1, therefore 6
        float multiplier = 1 / (0.005f * sensibility);
        value *= multiplier;
        if (value > 1) value = 1;
        if (value < -1) value = -1;

        return value;
    }
}