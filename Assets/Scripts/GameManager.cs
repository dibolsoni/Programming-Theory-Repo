using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int waveNumber = 0;
    public int lifeRemaining = 20;
    public int goldAmount = 0;
    public int time = 0;
    public int timePerWave = 20;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        time = timePerWave;
        StartCoroutine(Time());
    }

    IEnumerator Time()
    {
        while (true)
        {
            time -= 1;
            yield return new WaitForSeconds(1);
        }
    }

    private void LateUpdate()
    {
        if (time > timePerWave)
        {
            NextWave();
        }
    }

    public void NextWave()
    {
        waveNumber++;
        time = timePerWave;
    }

    public void AddGold(int gold)
    {
        this.goldAmount += gold;
    }

    public void removeLife(int life = 1)
    {
        this.lifeRemaining -= life;
        if (lifeRemaining <= 0)
            GameOver();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
