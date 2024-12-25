using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Speed settings
    public float startSpeed = 5f;
    public float maxSpeed = 10f;
    public float acceleration = 1f;
    public float deceleration = 2f;
    public float jumpForce;

    // UI elements
    public Text countText;
    public Text winText;
    public Text loseText;
    public Text speedText;

    private Rigidbody rb;
    private int count;
    private int totalPickups;
    // Current speed variable
    private float currentSpeed;

    // Reference to the main camera
    private Transform mainCamera;

    // Variable to check if the player is on the ground
    private bool isGrounded;

    void Start()
    {
        // Assign the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Assign the main camera's transform
        mainCamera = Camera.main.transform;

        // Set the initial pickup count and speed
        count = 0;
        currentSpeed = startSpeed;

        // Update UI
        SetCountText();
        winText.text = "";
        loseText.text = "";
        UpdateSpeedText();
        GameObject[] pickupObjects = GameObject.FindGameObjectsWithTag("Pick Up");
        totalPickups = pickupObjects.Length;
    }

    void FixedUpdate()
    {
        // Get player input
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Determine if there's any movement input
        bool isMoving = Mathf.Abs(moveHorizontal) > 0.01f || Mathf.Abs(moveVertical) > 0.01f;

        // Adjust speed based on input
        if (isMoving)
        {
            // Accelerate toward max speed
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // Decelerate back down to at least startSpeed
            currentSpeed -= deceleration * Time.deltaTime;
        }

        // Clamp currentSpeed between startSpeed and maxSpeed
        currentSpeed = Mathf.Clamp(currentSpeed, startSpeed, maxSpeed);

        // Get the forward and right direction relative to the camera
        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraRight = mainCamera.right;

        // Flatten the camera directions on the XZ plane
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement relative to the camera's orientation
        Vector3 movement = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized * currentSpeed;

        // Apply movement force
        rb.AddForce(movement);

        // Update the speed text UI
        UpdateSpeedText();
    }

    void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player picks up an object
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        // If the player hits a danger object
        else if (other.gameObject.CompareTag("Danger"))
        {
            loseText.text = "You Lose!";
            // Stop movement
			acceleration = 0;
			deceleration = 0;
			startSpeed = 0;
            currentSpeed = 0;
			jumpForce = 0;
            SetVelocity(0, 0, 0);
        }
    }

    void SetVelocity(float xVelocity, float yVelocity, float zVelocity)
    {
        rb.velocity = new Vector3(xVelocity, yVelocity, zVelocity);
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= totalPickups)
        {
            winText.text = "You Win!";
        }
    }

    void UpdateSpeedText()
    {
        speedText.text = "Speed: " + currentSpeed.ToString("F0");
    }
}
