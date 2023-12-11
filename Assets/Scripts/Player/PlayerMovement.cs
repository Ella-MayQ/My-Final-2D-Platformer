using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    // Movement parameters
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    // Coyote time parameters
    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    // Multiple jumps parameters
    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    // Wall jumping parameters
    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    // Layer masks
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    // Sound effect for jumping
    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    // Components
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    // Cooldown for wall jump
    private float wallJumpCooldown;
    private float horizontalInput;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Grab references from rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left-right
        if (horizontalInput < -0.04f)
            transform.localScale = new Vector3(4, 4, 4);
        else if (horizontalInput > 0.04f)
            transform.localScale = new Vector3(-4, 4, 4);

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // Jump
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();

        // Adjustable jump height
        if (Input.GetKeyUp(KeyCode.UpArrow) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 8;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; // Reset coyote counter when on the ground
                jumpCounter = extraJumps; // Reset jump counter to extra jump value
            }
            else
                coyoteCounter -= Time.deltaTime; // Start decreasing coyote counter when not on the ground
        }
    }

    // Method to handle jumping
    private void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;

        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                if (coyoteCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            coyoteCounter = 0;
        }
    }

    // Method to handle wall jumping
    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    // Method to check if the player is grounded
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // Method to check if the player is on a wall
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    // Method to check if the player can attack
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
