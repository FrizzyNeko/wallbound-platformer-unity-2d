using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;

    [SerializeField] float jumpSpeed = 20f;

    [SerializeField] float climbSpeed = 5f;

    [SerializeField] Vector2 deathKick = new Vector2(10f, 20f);

    [SerializeField] GameObject bullet;

    // Pozisyonunu almak için Transform
    [SerializeField] Transform gun;

    // Oyuncunun hareket isteðini tut
    Vector2 moveInput;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    float graviyScaleAtStart;

    bool isAlive = true;
    bool isFacingRight;
    bool jumpRequested;


    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        graviyScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) return;
        UpdateRunAnimation();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    private void FixedUpdate()
    {
        if (!isAlive) return;
        ApplyMovement();
        HandleJump();
    }

    // Input System çaðýrýr
    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        // Klavyeden / gamepad’den gelen yönü sakla
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;

        if (value.isPressed)
        {
            jumpRequested = true;
        }
    }

    void HandleJump()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        bool onGround = myFeetCollider.IsTouchingLayers(groundLayer);

        if (jumpRequested && onGround)
        {
            myRigidBody.linearVelocity = new Vector2(
                myRigidBody.linearVelocity.x,
                jumpSpeed
            );
        }

        jumpRequested = false;
    }

    void ApplyMovement()
    {
        // x yönü: oyuncunun isteði * hýz
        // y yönü: gravity ne diyorsa o
        Vector2 velocity = new Vector2(
            moveInput.x * moveSpeed,
            myRigidBody.linearVelocity.y
        );
        // Rigidbody’ye uygula
        myRigidBody.linearVelocity = velocity;
    }

    void UpdateRunAnimation()
    {
        bool hasHorizontalInput = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalInput);
    }


    void FlipSprite()
    {
        if (Mathf.Abs(moveInput.x) > Mathf.Epsilon)
        {
            isFacingRight = moveInput.x > 0;
            transform.localScale = new Vector2(
                isFacingRight ? 1f : -1f,
                1f
            );
        }
    }


    void ClimbLadder()
    {
        LayerMask climbingLayer = LayerMask.GetMask("Climbing");

        if (!myBodyCollider.IsTouchingLayers(climbingLayer))
        {
            myRigidBody.gravityScale = graviyScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        myRigidBody.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(myRigidBody.linearVelocity.x, moveInput.y * climbSpeed);
        myRigidBody.linearVelocity = climbVelocity;

        bool hasVerticalSpeed = Mathf.Abs(myRigidBody.linearVelocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", hasVerticalSpeed);
    }

    void OnAttack(InputValue value)
    {
        if (!isAlive) return;

        GameObject bulletObj = Instantiate(bullet, gun.position, transform.rotation);

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();

        float direction = transform.localScale.x; 

        bulletScript.Fire(direction);
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards", "Water")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidBody.linearVelocity = deathKick;
            FindAnyObjectByType<GameSession>().ProcessPlayerDeath();
        }
    }

  
}
