using UnityEngine;

public class RaycastMover : MonoBehaviour
{
    public float speed = 5f; // Speed of the object
    public float rayDistance = 0.5f; // Distance to cast the ray
    private Vector3 direction;

    void Start()
    {
        // Initialize the direction to diagonal (x and z)
        direction = new Vector3(1, 0, 1).normalized;
    }

    void Update()
    {
        // Cast a ray in the direction of movement
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("wall") || hit.collider.CompareTag("Danger"))
            {
                // Debug log to confirm detection
                Debug.Log($"Raycast hit wall: {hit.collider.name}");

                // Reflect the direction based on the hit normal
                direction = Vector3.Reflect(direction, hit.normal).normalized;

                // Ensure the Y-axis remains unchanged (optional for flat movement)
                direction.y = 0;
            }
        }

        // Move the object
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnDrawGizmos()
    {
        // Draw the ray in the Scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction * rayDistance);
    }
}
