using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;

    private TMP_Text label;
    private AudioSource audioSource;
    private int count;

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        label.text = count.ToString();
    }

    public void AddCoin()
    {
        count++;
        label.text = count.ToString();

        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }
    }
}