using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private GameObject winMessage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerMovement>(out var player))
            return;

        player.enabled = false;
        other.attachedRigidbody.simulated = false;
        winMessage.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}