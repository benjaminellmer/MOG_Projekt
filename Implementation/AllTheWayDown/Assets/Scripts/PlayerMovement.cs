using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float directionSpeed = 50f;
    [SerializeField] private float jumpForce = 25f;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float gyroSensibility = 30;
    [SerializeField] private float accSensibility = 50;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;

    // set to true, if the movement using <- -> and a d should be available
    [SerializeField] private bool debugOnPC; // TODO: Delete
    private Rigidbody rb;

    private void Start()
    {
        if (debugOnPC) ControlSettings.inputMethod = ControlSettings.InputMethod.PC; // ugly workaround
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        switch (ControlSettings.GetPreferredInputMethod())
        {
            case ControlSettings.InputMethod.PC:
                HandleMovementWithKeyboard();
                break;
            case ControlSettings.InputMethod.TOUCH:
                HandleMovementWithTouch();
                break;
            case ControlSettings.InputMethod.GYROSCOPE:
                /* Would be better, but does not work with unity remote
                if (SystemInfo.supportsGyroscope)
                    HandleMovementWithGyroscope();
                else
                    HandleMovementWithAccelerometer(); 
                */
                HandleMovementWithGyroscope();
                break;
            case ControlSettings.InputMethod.ACCELEROMETER:
                /* Would be better, but does not work with unity remote
                if (SystemInfo.supportsAccelerometer)
                    HandleMovementWithAccelerometer();
                else
                {
                    HandleMovementWithTouch();
                }
                */
                HandleMovementWithAccelerometer();
                break;
        }

        GameManager.inst.IncMeters(transform.position.z);
    }

    private void HandleMovementWithKeyboard()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        MovePlayer(horizontalInput, Input.GetButtonDown("Jump"));
    }

    private void HandleMovementWithAccelerometer()
    {
        float horizontalInput = ApplySensibility(Input.acceleration.x, accSensibility);
        MovePlayer(horizontalInput, Input.touchCount > 0);
    }

    private void HandleMovementWithTouch()
    {
        float horizontalInput = 0;
        var jumping = false;

        if (Input.touchCount > 0)
            foreach (var touch in Input.touches)
            {
                if (touch.position.y < Screen.height / 3)
                {
                    horizontalInput = (Input.touches[0].position.x - 500) / 1000 * 2;
                }
                else
                {
                    jumping = true;
                }
            }

        MovePlayer(horizontalInput, jumping);
    }

    private void HandleMovementWithGyroscope()
    {
        // Movement controls using the Gyroscope of the Phone
        // retrieved from: https://elearning.fh-ooe.at/pluginfile.php/486729/mod_resource/content/0/07_controls_notes.pdf slide 35
        Input.gyro.enabled = true;
        var yawPitchRoll = Input.gyro.attitude.eulerAngles;
        Debug.Log(yawPitchRoll);

        // read angle in range [0, 360]
        var rawValueY = yawPitchRoll.y;

        // convert values to [-180,180]
        if (rawValueY > 180) rawValueY -= 360;
        if (rawValueY < -180) rawValueY += 360;

        // scale to angle
        rawValueY = Mathf.Clamp(rawValueY / 90, -1, 1);
        rawValueY = ApplySensibility(rawValueY, gyroSensibility);

        MovePlayer(rawValueY, Input.touchCount > 0);
    }

    private void MovePlayer(float horizontalInput, bool jumpActive)
    {
        var velX = horizontalInput * directionSpeed;
        var velY = rb.velocity.y;
        var velZ = forwardSpeed;

        if (PlayerIsGrounded())
        {
            if (jumpActive) velY = jumpForce;
        }
        else
        {
            if (transform.position.y < 1)
            {
                velZ = 0;
                velX = 0;
            }
        }

        rb.velocity = new Vector3(velX, velY, velZ);
    }

    /**
     * The sensibility defines the value at, which the whole directional speed should be applied.
     * If the sensibility is 30 the whole directional speed is applied if the phone is tilted 30 degrees.
     */
    private float ApplySensibility(float value, float sensibility)
    {
        // to avoid DivideByZero
        if (value == 0) return 0;

        // The value should already be 1 at 30 degree rotation, therefore multiply and then modify it again
        // 180deg = 1, per deg 0.005, 30 deg = 0.16, 0.16 should already be 1, therefore 6
        var multiplier = 1 / (0.005f * sensibility);
        value *= multiplier;
        if (value > 1) value = 1;
        if (value < -1) value = -1;

        return value;
    }

    /*
     * This function checks if the rb is touching the ground (a element with the ground layer)
     */
    private bool PlayerIsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.4f, ground);
    }
}