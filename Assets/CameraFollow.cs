using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lookAhead = 2f;
    [SerializeField] private float smoothTime = 0.15f;

    private float horizontalVelocity;

    private void Start()
    {
        Vector3 position = transform.position;
        position.x = player.position.x + lookAhead;
        transform.position = position;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = Mathf.SmoothDamp(
            position.x,
            player.position.x + lookAhead,
            ref horizontalVelocity,
            smoothTime);
        transform.position = position;
    }
}