using UnityEngine;

public class RaceObstacles : MonoBehaviour
{
    public float moveSpeed = 4f; // Speed of movement
    public float minX = -7f; // Minimum X position
    public float maxX = 7f; // Maximum X position
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
                Mathf.MoveTowards(transform.position.x, maxX, step),
                transform.position.y,
                transform.position.z
            );

            // Check if the object reached the right edge
            if (Mathf.Approximately(transform.position.x, maxX))
            {
                movingRight = false; // Change direction to left
            }
        }
        else
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, minX, step),
                transform.position.y,
                transform.position.z
            );

            // Check if the object reached the left edge
            if (Mathf.Approximately(transform.position.x, minX))
            {
                movingRight = true; // Change direction to right
            }
        }
    }
}
