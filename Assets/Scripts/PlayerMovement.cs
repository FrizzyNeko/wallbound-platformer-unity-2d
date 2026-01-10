using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 8f;

    [SerializeField]
    float jumpSpeed = 23f;

    [SerializeField]
    float climbSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;

    float graviyScaleAtStart;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        graviyScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        bool onGround = myCapsuleCollider.IsTouchingLayers(groundLayer);

        if (value.isPressed && onGround)
        {
            myRigidBody.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        // Tuþa bastýðýmýzda girilen input'un x deðerini al. y deðerini rigidbody'de ne yazýyorsa öyle býrak. Yani Gravity 1.
        //Hareket etme kodu.
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.linearVelocity.y);
        myRigidBody.linearVelocity = playerVelocity;

        // Koþuyor muyuz? Animasyonu oynat.
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void FlipSprite()
    {
        // x hýzýmýz ne olursa olsun pozitif döner. bu dönen rakam en küçük float'tan büyükse true döner.
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;

        // true ise sprite'ý sola veya saða çevir.
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.linearVelocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        LayerMask climbingLayer = LayerMask.GetMask("Climbing");

        if (!myCapsuleCollider.IsTouchingLayers(climbingLayer))
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
}
