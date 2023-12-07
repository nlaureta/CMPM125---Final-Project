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
    [SerializeField]
    private int playerIndex = 0;
    private Vector2 inputVector = Vector2.zero;

    bool moveLeft = false;
    bool moveRight = false;
    bool keyboardJumpButton = false;

    Animator anims;
    private bool isBlocking = false;
    private float baseSpeed, blockSpeed;
    private bool knockedBack = false;

    void Awake()
    {
        gamepadControls = new Player1Controls();
        //gamepadControls.Player1Gameplay.Move.performed += ctx => gamepadMove = ctx.ReadValue<Vector2>();
        //gamepadControls.Player1Gameplay.Move.canceled += ctx => gamepadMove = Vector2.zero;
        gamepadControls.Player1Gameplay.Move.performed += ctx => gamepadJump();
        anims = GetComponentInChildren<Animator>();
        baseSpeed = moveSpeed;
        blockSpeed = moveSpeed / 3;
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction)
    {
        anims.SetBool("Moves", true);
        inputVector = direction;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void gamepadJump()
    {
        if (inputVector.y > 0.5f && isGrounded)
        {
            anims.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Update the character's position
        float horizontalInput = 0f;
        isBlocking = anims.GetBool("Block");
        knockedBack = anims.GetBool("Knocked");
        if (isBlocking)
        {
            moveSpeed = blockSpeed;
        }
        else
        {
            moveSpeed = baseSpeed;
        }
        //Debug.Log(inputVector.x);
        if (inputVector.x > 0.1f || inputVector.x < 0f)
        {
            horizontalInput = inputVector.x;
        }
        else
        {
            anims.SetBool("Moves", false);
            if (playerIndex == 0)
            {
                // Get input from the player
                moveLeft = Input.GetKey(KeyCode.A);
                moveRight = Input.GetKey(KeyCode.D);
                keyboardJumpButton = Input.GetKey(KeyCode.W);

            }
            else if (playerIndex == 1)
            {
                // Get input from the player
                moveLeft = Input.GetKey(KeyCode.LeftArrow);
                moveRight = Input.GetKey(KeyCode.RightArrow);
                keyboardJumpButton = Input.GetKey(KeyCode.UpArrow);
            }

            if (moveLeft)
            {
                anims.SetBool("Moves", true);
                horizontalInput = -1f;
            }
            else if (moveRight)
            {
                anims.SetBool("Moves", true);
                horizontalInput = 1f;
            }

            // Jumping with the "W or Up Arrow" key
            if (keyboardJumpButton && isGrounded && !isBlocking)
            {
                anims.SetTrigger("Jump");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

        }

        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, 0f);


        // Face the enemy
        if (enemy != null && !knockedBack)
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
