using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody player;
    [SerializeField]float directionSped = 5f;
    [SerializeField]float jumpForce = 5f;
    [SerializeField]float forwardSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        player.velocity = new Vector3(horizontalInput * directionSped, player.velocity.y, forwardSpeed);
        
        if (Input.GetButtonDown("Jump"))
        {
            player.velocity = new Vector3(horizontalInput * directionSped, jumpForce, forwardSpeed);
        }
    }
}
