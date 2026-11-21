using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float fallLimit = -5f;

    private Rigidbody2D body;
    private Vector2 respawnPosition;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        respawnPosition = body.position;
    }

    private void FixedUpdate()
    {
        if (body.position.y < fallLimit)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        body.position = respawnPosition;
        body.linearVelocity = Vector2.zero;
    }

    public void SetCheckpoint(Vector2 position)
    {
        respawnPosition = position;
    }
}