using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    // Store a public reference to the Player game object, so we can refer to its Transform
    public GameObject player;

    // Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
    private Vector3 offset;

    // Speed of horizontal camera movement
    public float rotationSpeed = 0.5f;

    // At the start of the game...
    void Start()
    {
        // Create an offset by subtracting the Camera's position from the player's position
        offset = transform.position - player.transform.position;
    }

    // After the standard 'Update()' loop runs, and just before each frame is rendered...
    void LateUpdate()
    {
        // Check if the right mouse button is held down
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            // Get horizontal mouse movement
            float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;

            // Rotate the offset around the player based on mouse movement
            offset = Quaternion.AngleAxis(horizontal, Vector3.up) * offset;
        }

        // Set the position of the Camera (the game object this script is attached to)
        // to the player's position, plus the offset amount
        transform.position = player.transform.position + offset;

        // Make the camera look at the player
        transform.LookAt(player.transform);
    }
}
