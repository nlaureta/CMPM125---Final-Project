using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust this to set the movement speed
    public float jumpForce = 10f; // Adjust this to set the jump force
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

        // Update the character's position (only sideways movement)
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, 0f);

        // Align the character's rotation with the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 1000f);
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






