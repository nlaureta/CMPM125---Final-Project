using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust the speed as needed

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Freeze rotation on X and Z axes to keep the player upright
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Player movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontalMovement, 0, 0);
        rb.velocity = moveDirection * moveSpeed;
    }
}
