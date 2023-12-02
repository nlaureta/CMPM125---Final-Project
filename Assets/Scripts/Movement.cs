using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust this to set the movement speed
    public float jumpForce = 20f; // Adjust this to set the jump force
    public Transform enemy; // Drag and drop the enemy's transform in the Inspector
    private Rigidbody rb;
    private bool isGrounded = true; // Flag to check if the character is grounded

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");

        // Update the character's position
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




            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // Set a fixed rotation of -90 degrees around the y-axis because the arm is not the way player is facing might change later
            targetRotation *= Quaternion.Euler(0, -90, 0);





            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 1000f);
        }

        // Jumping with the "W" key
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Check if the character is grounded
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
