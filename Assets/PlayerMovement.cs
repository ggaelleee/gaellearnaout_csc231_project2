using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpSpeed = 6f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private BoxCollider2D playerCollider;
    private float moveDirection;
    private bool jumpRequested;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
        {
            moveDirection = 0f;
            jumpRequested = false;
            return;
        }

        bool left = keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed;
        bool right = keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed;

        moveDirection = (right ? 1f : 0f) - (left ? 1f : 0f);

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = body.linearVelocity;
        velocity.x = moveDirection * moveSpeed;

        if (jumpRequested && playerCollider.IsTouchingLayers(groundLayer))
        {
            velocity.y = jumpSpeed;
        }

        jumpRequested = false;
        body.linearVelocity = velocity;
    }
}