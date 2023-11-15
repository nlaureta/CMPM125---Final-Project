using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust this to set the movement speed
    public float jumpForce = 3f; // Adjust this to set the jump force
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
        float verticalInput = Input.GetAxis("Vertical");

        // Update the character's position
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();  // Normalize to prevent faster diagonal movement
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);

        // Align the character's rotation with the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 1000f);
        }

        // Flip the character based on movement direction
        if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 0.59f, 0.59f);
        else if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 0.59f, 0.59f);

        // Jumping
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