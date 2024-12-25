using UnityEngine;
using UnityEngine.UI;

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
    private float currentSpeed;

    private Transform mainCamera;
    private bool isGrounded;

    // Reference to the GameManager
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main.transform;

        count = 0;
        currentSpeed = startSpeed;

        SetCountText();
        winText.text = "";
        loseText.text = "";
        UpdateSpeedText();

        GameObject[] pickupObjects = GameObject.FindGameObjectsWithTag("Pick Up");
        totalPickups = pickupObjects.Length;

        // Find and assign the GameManager (ensure it's in the scene)
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        bool isMoving = Mathf.Abs(moveHorizontal) > 0.01f || Mathf.Abs(moveVertical) > 0.01f;

        if (isMoving)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, startSpeed, maxSpeed);

        Vector3 cameraForward = mainCamera.forward;
        Vector3 cameraRight = mainCamera.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized * currentSpeed;

        rb.AddForce(movement);
        UpdateSpeedText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
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
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Danger"))
        {
            loseText.text = "You Lose!";
            acceleration = 0;
            deceleration = 0;
            startSpeed = 0;
            currentSpeed = 0;
            jumpForce = 0;
            SetVelocity(0, 0, 0);

            // Reload the current level on loss
            gameManager?.ReloadLevel();
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
            // Load the next level when the player wins
            gameManager?.LoadNextLevel();
        }
    }

    void UpdateSpeedText()
    {
        speedText.text = "Speed: " + currentSpeed.ToString("F0");
    }
}
