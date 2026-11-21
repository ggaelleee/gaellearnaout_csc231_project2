using UnityEngine;

public class CheckpointFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerRespawn>(out var player))
        {
            player.SetCheckpoint((Vector2)transform.position + Vector2.up);
        }
    }
}