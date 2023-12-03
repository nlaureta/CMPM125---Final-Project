using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    Player1Controls gamepadControls;
    public float moveSpeed = 5f;
    public float jumpForce = 20f;
    public Transform enemy;
    private Rigidbody rb;
    private bool isGrounded = true;
    Vector2 gamepadMove;

    void Awake()
    {
        gamepadControls = new Player1Controls();
        gamepadControls.Player1Gameplay.Move.performed += ctx => gamepadMove = ctx.ReadValue<Vector2>();
        gamepadControls.Player1Gameplay.Move.canceled += ctx => gamepadMove = Vector2.zero;
        gamepadControls.Player1Gameplay.Move.performed += ctx => gamepadJump();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void gamepadJump()
    {
        if (gamepadMove.y > 0.5f && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void Update()
    {
        //Debug.Log(gamepadMove.y);
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


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnEnable()
    {
        gamepadControls.Player1Gameplay.Enable();
    }

    void OnDisable()
    {
        gamepadControls.Player1Gameplay.Disable();
    }
}
