using UnityEngine;

public class SpriteMover : MonoBehaviour
{
    public float speed = 5.0f; // Adjust movement speed
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody component
    }

    void Update()
    {
        // Get input from WASD / Arrow keys
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Create a movement vector (keeping Y fixed)
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // Move using transform (without Rigidbody)
        transform.position += moveDirection * speed * Time.deltaTime;

        // If using Rigidbody, apply velocity instead
        if (rb != null)
        {
            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
        }
    }
}
