using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody player;
    [SerializeField] private float directionSpeed = 50f;
    [SerializeField] private float jumpForce = 25f;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float sensibility = 30;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;

    private bool jumping = false;
    private float jumpTime = 0;

    // set to true, if the movement using <- -> and a d should be available
    [SerializeField] private bool debugOnPC = false;

    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    void Update()
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
        MovePlayer(horizontalInput, Input.GetButtonDown("Jump"));
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
        rawValueY = ApplySensibility(rawValueY);
        
        MovePlayer(rawValueY, Input.touchCount > 0);
    }

    void MovePlayer(float horizontalInput, bool jumpActive)
    {

        if (jumping) jumpTime += Time.deltaTime;

        float velX = horizontalInput * directionSpeed;
        float velY = player.velocity.y;
        float velZ = forwardSpeed;
        
        if (PlayerIsGrounded())
        {
            if (!JumpJustStarted())
            {
                jumping = false;
                jumpTime = 0;
            }

            if (jumpActive)
            {
                velY = jumpForce;
                jumping = true;
            }
        }
        else
        {
            if (!jumping)
            {
                velZ = 3;
                velX = 0;
            }
        }
        player.velocity = new Vector3(velX, velY, velZ);
    }

    /**
     * The sensibility defines the value at, which the whole directional speed should be applied.
     * If the sensibility is 30 the whole directional speed is applied if the phone is tilted 30 degrees.
     */
    private float ApplySensibility(float value)
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
    
    /*
     * This function checks if the player is touching the ground (a element with the ground layer)
     */
    private bool PlayerIsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.45f, ground);
    }

    /*
     * This function is used, to check if the player just initialized the jump.
     * The problem with PlayerIsGrounded is, that it can be true the first frames after the jump
     * was initialized, therefore it is checked if the jump action is longer ago then 0.3 seconds
     */
    private bool JumpJustStarted()
    {
        return jumpTime < 0.3;
    }
    
}