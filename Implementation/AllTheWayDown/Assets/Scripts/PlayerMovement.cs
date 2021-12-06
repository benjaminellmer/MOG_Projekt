using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody player;
    [SerializeField] float directionSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float forwardSpeed = 2f;

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
            float horizontalInput = Input.GetAxis("Horizontal");
            player.velocity = new Vector3(horizontalInput * directionSpeed, player.velocity.y, forwardSpeed);

            // TODO: Check if the player is already jumping, should only be possible to jump, if the player touches the collider of the ground
            if (Input.GetButtonDown("Jump"))
            {
                player.velocity = new Vector3(horizontalInput * directionSpeed, jumpForce, forwardSpeed);
            }
        }
        else
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
            
            // Only the first 30 degrees will be considered, because the movement has to be very sensible
            
            // move body
            Vector3 forwardMove = transform.forward * forwardSpeed * Time.deltaTime;
            Vector3 sideMove = transform.right * rawValueY * directionSpeed * Time.deltaTime;
            player.MovePosition(transform.position + forwardMove + sideMove);
        }
    }
}