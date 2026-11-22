using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float fallLimit = -5f;
    [SerializeField] private TMP_Text livesCounter;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float hurtDuration = 0.45f;

    private Rigidbody2D body;
    private PlayerMovement movement;
    private Animator animator;
    private Vector2 respawnPosition;
    private int lives = 3;
    private bool isRespawning;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();

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
        if (isRespawning || lives == 0)
        {
            return;
        }

        isRespawning = true;
        StartCoroutine(PlayHurtAndRespawn());
    }

    private IEnumerator PlayHurtAndRespawn()
    {
        lives--;
        livesCounter.text = $"Lives: {lives}";

        movement.enabled = false;
        body.linearVelocity = Vector2.zero;
        body.simulated = false;

        animator.Play("KnightHurt", 0, 0f);
        yield return new WaitForSeconds(hurtDuration);

        if (lives == 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            yield break;
        }

        body.simulated = true;
        body.position = respawnPosition;
        body.linearVelocity = Vector2.zero;
        movement.enabled = true;
        isRespawning = false;
    }

    public void SetCheckpoint(Vector2 position)
    {
        respawnPosition = position;
    }
}