using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float jumpForce = 10.0f;
    private Vector3 movement;
    private Vector3 rotatedMovement;
    private bool isGrounded;

    void Update()
    {
        GetInput();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f;
        movement.z = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }

    void Movement()
    {
        rotatedMovement = Quaternion.Euler(0, 45, 0) * movement;

        rigidbody.MovePosition(transform.position + rotatedMovement * movementSpeed * Time.deltaTime);

        if (rotatedMovement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rotatedMovement);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
