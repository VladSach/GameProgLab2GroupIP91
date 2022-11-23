using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdate : MonoBehaviour
{
    private int playerCoins = 0;
    private TextMeshProUGUI coinsText;
    
    public void SetPlayerCoins(int coins)
    {
        playerCoins = coins;
    }

    private void Awake()
    {
        coinsText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        coinsText.text = "Coins: " + playerCoins.ToString();
    }

}
