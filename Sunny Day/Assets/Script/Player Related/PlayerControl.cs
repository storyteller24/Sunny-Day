using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // Make this a serialized field so you can drag and drop the asset in the Inspector
    [SerializeField] private InputActionAsset playerControlsAsset;

    // Private variables to hold the active action map and its actions
    private InputActionMap playerMap;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Animator anim;

    private Vector2 moveInput;
    private bool isFacingRight = true;
    private Vector2 Platform;
    private bool onPlatform = true;


    [Header("Movement Properties")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private int maxJumps = 2;
    private int jumpCount;

    [Header("Ground Check")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private void Awake()
    {
        // Find the "Player" action map from the asset
        playerMap = playerControlsAsset.FindActionMap("Player");

        // Find the specific actions by their names
        moveAction = playerMap.FindAction("Move");
        jumpAction = playerMap.FindAction("Jump");

        // Subscribe to the events using the retrieved actions
        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;

        jumpAction.performed += ctx => OnJump();
        jumpAction.canceled += ctx => OnJumpReleased();

         // Get the Rigidbody2D component for platform collision handling
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerMap.Enable(); // Enable the entire action map
    }

    private void OnDisable()
    {
        playerMap.Disable(); // Disable the entire action map
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = maxJumps;
    }
    private void Update()
    {
        if (IsGrounded())
        {
            jumpCount = maxJumps;
        }
        Flip();    
    }

    private void FixedUpdate()
    {
        if (onPlatform)
        {
            
            rb.linearVelocity = new Vector2(moveInput.x * speed + Platform.x, rb.linearVelocity.y);
        }
        else
        {
            
            rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        }
        
        PhysicAdapt();

    #region Animation
        if (IsGrounded())
        {
            if (moveInput.x == 0f)
            {
                anim.Play("Idle");
            }
            else
            {
                anim.Play("Run");
            }

        }
        else
        {
            anim.Play("Jump");
        }
    }
    #endregion
    void PhysicAdapt()
    {
        if (IsGrounded())
        {
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Moving Platform") && IsGrounded())
        {
            onPlatform = true;
            Rigidbody2D platformRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Platform = new Vector2(platformRb.linearVelocity.x, platformRb.linearVelocity.y);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Moving Platform"))
        {
            onPlatform = true;
            Rigidbody2D platformRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Platform = new Vector2(platformRb.linearVelocity.x, platformRb.linearVelocity.y);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Moving Platform"))
        {
            onPlatform = false;
            Platform = Vector2.zero; // Reset the platform velocity when exiting
        }
    }

  

    // The rest of the private methods remain the same
    private void OnJump()
    {
        if (IsGrounded() || (!IsGrounded() && jumpCount >0))
        {
            jumpCount--;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
        
    }

    private void OnJumpReleased()
    {
        if (rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && moveInput.x < 0f || !isFacingRight && moveInput.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
