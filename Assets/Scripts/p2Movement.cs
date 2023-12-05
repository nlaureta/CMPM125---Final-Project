using UnityEngine;

public class p2Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform enemy;
    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from the player
        bool moveLeft = Input.GetKey(KeyCode.LeftArrow);
        bool moveRight = Input.GetKey(KeyCode.RightArrow);

        // Update the character's position
        float horizontalInput = 0f;

        if (moveLeft)
        {
            horizontalInput = -1f;
        }
        else if (moveRight)
        {
            horizontalInput = 1f;
        }

        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, 0f);

        // Face the enemy
        if (enemy != null)
        {
            // Calculate the target position relative to the player
            Vector3 relativeEnemyPosition = enemy.position - transform.position;
            relativeEnemyPosition.y = 0f; // Ignore the vertical component

            // Use the relative position to face the enemy
            Quaternion targetRotation = Quaternion.LookRotation(relativeEnemyPosition, Vector3.up);

            // Set a fixed rotation of -90 degrees around the y-axis because the arm is not the way player is facing might change later
            targetRotation *= Quaternion.Euler(0, 90, 0);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 1000f);
        }

        // Jumping with the up arrow key
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
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
}
