using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private TMP_Text label;
    private int count;

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
        label.text = "Coins: 0";
    }

    public void AddCoin()
    {
        count++;
        label.text = $"Coins: {count}";
    }
}