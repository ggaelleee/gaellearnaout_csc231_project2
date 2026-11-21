using UnityEngine;

public class SkeletonPatrol : MonoBehaviour
{
    [SerializeField] private float leftEdge = 15f;
    [SerializeField] private float rightEdge = 17.5f;
    [SerializeField] private float speed = 1.25f;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private bool movingRight = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 position = body.position;
        float destination = movingRight ? rightEdge : leftEdge;
        position.x = Mathf.MoveTowards(
            position.x, destination, speed * Time.fixedDeltaTime);

        body.MovePosition(position);

        if (position.x == destination)
        {
            movingRight = !movingRight;
            sprite.flipX = !movingRight;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerRespawn>(out var player))
        {
            player.Respawn();
        }
    }
}