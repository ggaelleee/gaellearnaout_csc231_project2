using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private CoinCounter counter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerRespawn>(out _))
        {
            gameObject.SetActive(false);
            counter.AddCoin();
        }
    }
}