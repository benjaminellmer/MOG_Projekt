using UnityEditor.UIElements;
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

    [SerializeField] private bool debugOnPC; // TODO: Delete
    private Rigidbody rb;
    private ControlSettings.InputMethod inputMethod;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Fetch once at the beginning, so it does not have to be loaded from the player prefs, each frame
        inputMethod = ControlSettings.GetPreferredInputMethod();
        if (inputMethod == ControlSettings.InputMethod.Gyroscope)
        {
            // Initialize gyroscope
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 1f / 60; // 60Hz
        }
    }

    private void Update()
    {
        if (debugOnPC) HandleMovementWithKeyboard();
        else
        {
            switch (inputMethod)
            {
                case ControlSettings.InputMethod.Touch:
                    HandleMovementWithTouch();
                    break;
                case ControlSettings.InputMethod.Gyroscope:
                    HandleMovementWithGyroscope();
                    break;
                case ControlSettings.InputMethod.Accelerometer:
                    HandleMovementWithAccelerometer();
                    break;
            }
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
        var yawPitchRoll = Input.gyro.attitude.eulerAngles;

        // read angle in range [0, 360]
        var rawValueY = yawPitchRoll.y;

        // convert values to [-180,180]
        if (rawValueY > 180) rawValueY -= 360;
        if (rawValueY < -180) rawValueY += 360;

        // scale to angle
        rawValueY = Mathf.Clamp(rawValueY / 90, -1, 1);
        rawValueY = ApplySensibility(rawValueY, gyroSensibility);

        if (ControlSettings.GetMirrorGyro()) rawValueY *= -1;

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