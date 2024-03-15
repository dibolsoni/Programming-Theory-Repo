using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    // ENCAPLUSATION
    public int lifeRemaining { get; private set; }
    // ENCAPLUSATION
    public int goldAmount { get; private set; }
    private int _bestScore = 0;
    // ENCAPLUSATION
    public int bestScore
    {
        get { return _bestScore; }
        set
        {
            if (value > _bestScore)
            {
                _bestScore = value;
            }
        }
    }
    // ENCAPLUSATION
    public string playerName { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        lifeRemaining = GameManager.Instance.startLife;
        goldAmount = GameManager.Instance.startGold;
        playerName = "Player";
    }

    public void increaseGold(int amount)
    {
        if (amount < 0)
        {
            return;
        }
        goldAmount += amount;
    }

    public void decreaseGold(int amount)
    {
        int changed = goldAmount - amount;
        if (changed < 0)
        {
            Debug.Log("Decrease gold negative: " + changed);
            goldAmount = 0;
            return;
        }
        goldAmount -= amount;
    }


    public void increaseLife(int amount)
    {
        lifeRemaining += amount;
    }

    public void decreaseLife(int amount)
    {
        if (lifeRemaining - amount < 0)
        {
            lifeRemaining = 0;
            return;
        }
        lifeRemaining -= amount;
    }



}