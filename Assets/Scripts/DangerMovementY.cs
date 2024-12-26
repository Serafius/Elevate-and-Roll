using UnityEngine;

public class DangerMovementY : MonoBehaviour
{
    public float moveSpeed = 4f; // Speed of movement
    public float minY = -3.9f; // Minimum Y position
    public float maxY = 0f; // Maximum Y position
    public bool startMovingRight = true; // Option to set initial direction

    private bool movingRight; // Determines the initial direction of movement

    void Start()
    {
        // Use the configurable option to set the initial direction
        movingRight = startMovingRight;
    }

    void Update()
    {
        // Calculate the movement step
        float step = moveSpeed * Time.deltaTime;

        // Determine the direction and move the object
        if (movingRight)
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.MoveTowards(transform.position.y, maxY, step),
                transform.position.z
            );

            // Check if the object reached the right edge
            if (Mathf.Approximately(transform.position.y, maxY))
            {
                movingRight = false; // Change direction to left
            }
        }
        else
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.MoveTowards(transform.position.y, minY, step),
                transform.position.z
            );

            // Check if the object reached the left edge
            if (Mathf.Approximately(transform.position.y, minY))
            {
                movingRight = true; // Change direction to right
            }
        }
    }
}
