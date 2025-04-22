using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// where it would be for the player movment and Jumping for the player.
public class Player : MonoBehaviour
{
    //camera rotation
    public float mouseSensitivity = 1f;
    private float verticalRatation = 0f;
    private Transform cameraTransform;

    // ground movement 
    private Rigidbody rb;
    public float MoveSpeed = 5f;
    private float moveHorizontal; 
    private float moveForward; 

    //jumping
    public float jumpForce = 5f;
    public float fallMultiplier = 1.5f; // this would be for the graverty of whan the player would be falling 
    private bool isGrounded = true;
    public LayerMask groundLayer;
    // this would be for the player to be on the ground how for the gravet for the player falling 
    private float groundCheckTimer = 0f;
    //for to be a delay for it rester whn on the ground and hopw that it would have the 
    private float groundCheckDelay =0.3f; 
    private float playerHeight;
    private float raycastDistance;
    
    private float ascendMultiplier;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;

        // so ther wount be the mouse on the screen.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;   

        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;     
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        if ( Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
        }

        // it would be seeing if the player is on the fround with the ground check delay
        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

        
    }
    void FixedUpdate()
    {
        Moveplayer();
        ApplyJumpPhysics();
    }
   void Moveplayer()
   {


     Vector3 movement = (transform.right* moveHorizontal + transform.forward * moveForward). normalized;
     Vector3 targetVelocity = movement * MoveSpeed;

     //put movement to rigidbody
     Vector3 velocity = rb.velocity;
     velocity.x = targetVelocity.x;
     velocity.z = targetVelocity.z;
     rb.velocity = velocity;

     // where that it would make it so that it would not slidenot moving
     if ( isGrounded && moveHorizontal == 0 && moveForward == 0)
     {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

     }
   }
   void RotateCamera()
   {
     float horizonatalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
     transform.Rotate(0, horizonatalRotation, 0);

     verticalRatation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
     verticalRatation = Mathf.Clamp(verticalRatation, -90f, 90f);

     cameraTransform.localRotation = Quaternion.Euler(verticalRatation, 0, 0);
   }

   void jump()
   {
     isGrounded = false;
     groundCheckTimer = groundCheckDelay;
     rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);// for the jump
   }

   void ApplyJumpPhysics()
   {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
   }
}
