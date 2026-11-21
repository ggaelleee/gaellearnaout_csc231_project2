using TMPro;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float fallLimit = -5f;
    [SerializeField] private TMP_Text livesCounter;
    [SerializeField] private GameObject gameOverPanel;

    private Rigidbody2D body;
    private Vector2 respawnPosition;
    private int lives = 3;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        respawnPosition = body.position;
        livesCounter.text = $"Lives: {lives}";
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
        if (lives == 0)
            return;

        lives--;
        livesCounter.text = $"Lives: {lives}";
        body.linearVelocity = Vector2.zero;

        if (lives == 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            return;
        }

        body.position = respawnPosition;
    }

    public void SetCheckpoint(Vector2 position)
    {
        respawnPosition = position;
    }
}