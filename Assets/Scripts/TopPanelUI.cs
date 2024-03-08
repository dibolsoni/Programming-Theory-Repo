using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    [SerializeField]
    private int waveNumber;
    [SerializeField]
    private int goldAmount;
    [SerializeField]
    private int timer;
    [SerializeField]
    private int lifeRemaining;

    public TextMeshProUGUI waveNumberElement;
    public TextMeshProUGUI goldAmountElement;
    public TextMeshProUGUI timerElement;
    public TextMeshProUGUI lifeElement;

    private void Start()
    {
        waveNumber = 0;
        goldAmount = 0;
        timer = 0;
        lifeRemaining = 0;
    }

    private void LateUpdate()
    {
        GameManager gameState = GameManager.Instance;
        PlayerManager playerState = PlayerManager.Instance;
        UpdateWaveNumber(gameState.waveNumber);
        UpdateTimer(gameState.time);  
        UpdateGoldAmount(playerState.goldAmount);
        UpdateLife(playerState.lifeRemaining);
    }

    private void UpdateWaveNumber(int wnumber)
    {
        waveNumber = wnumber;
        waveNumberElement.text = "Wave: " + waveNumber;
    }

    private void UpdateGoldAmount(int gAmount)
    {
        goldAmount = gAmount;
        goldAmountElement.text = "Gold: " + goldAmount;
    }

    private void UpdateLife(int life)
    {
        lifeRemaining = life;
        lifeElement.text = "Life: " + lifeRemaining;
    }

    private void UpdateTimer(int t)
    {
        timer = t;
        timerElement.text = "Time: " + timer;
    }
}
