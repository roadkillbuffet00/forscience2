using System.Runtime.CompilerServices;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    float xRotation = 0f;
    [SerializeField] Transform playerCamera; // Assign your camera in the Inspector
     Rigidbody rb;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor in place 
    }

    // Update is called once per frame
    // Handle mouse look
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X")* mouseSensitivity * Time.deltaTime;
        float mouseY= Input.GetAxis("Mouse Y")* mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical rotation

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotate camera up/down
        transform.Rotate(Vector3.up * mouseX); // Rotate player left/right
        // Move in the direction the player is facing
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        rb.velocity= new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection. z * movementSpeed);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }
    
            void Jump ()
            { 
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

  
    bool IsGrounded()
    {
       return  Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

}