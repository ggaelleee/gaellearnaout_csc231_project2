using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpSpeed = 6f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rollSpeed = 8f;
    [SerializeField] private float rollDuration = 0.35f;

    private Rigidbody2D body;
    private BoxCollider2D playerCollider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float moveDirection;
    private float facingDirection = 1f;
    private float rollTimeLeft;
    private bool jumpRequested;
    private bool rollRequested;
    private string currentAnimation;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            moveDirection = 0f;
            return;
        }

        bool left = keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed;
        bool right = keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed;
        moveDirection = (right ? 1f : 0f) - (left ? 1f : 0f);

        jumpRequested |= keyboard.spaceKey.wasPressedThisFrame;
        rollRequested |= keyboard.leftShiftKey.wasPressedThisFrame;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = body.linearVelocity;
        bool grounded = velocity.y <= 0.01f &&
                        playerCollider.IsTouchingLayers(groundLayer);

        if (rollTimeLeft <= 0f)
        {
            if (moveDirection != 0f)
            {
                facingDirection = moveDirection;
                spriteRenderer.flipX = facingDirection < 0f;
            }

            if (grounded && rollRequested)
            {
                rollTimeLeft = rollDuration;
            }
            else if (grounded && jumpRequested)
            {
                velocity.y = jumpSpeed;
                grounded = false;
            }
        }

        bool rolling = rollTimeLeft > 0f;
        if (rolling)
        {
            velocity.x = facingDirection * rollSpeed;
            rollTimeLeft -= Time.fixedDeltaTime;
            PlayAnimation("KnightRoll");
        }
        else
        {
            velocity.x = moveDirection * moveSpeed;

            if (!grounded)
                PlayAnimation(velocity.y > 0f ? "KnightJump" : "KnightFall");
            else
                PlayAnimation(moveDirection == 0f ? "KnightIdle" : "KnightRun");
        }

        body.linearVelocity = velocity;
        jumpRequested = false;
        rollRequested = false;
    }

    private void PlayAnimation(string stateName)
    {
        if (currentAnimation == stateName)
            return;

        animator.Play(stateName, 0, 0f);
        currentAnimation = stateName;
    }
}