using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int waveNumber
    {
        get
        {
            return SpawnManager.Instance.WaveNumber;
        }
    }

    public int startLife = 20;
    
    // ENCAPLUSATION
    public int lifeRemaining { get; private set;}

    public int startGold = 10;
    // ENCAPLUSATION
    public int goldAmount { get; private set; }

    public int time;
    public int timePerWave = 20;

    public bool isGameOver = false;

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
        lifeRemaining = startLife;
        goldAmount = startGold;
        time = 0;
        isGameOver = false;
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
        time = timePerWave;
    }

    public void changeGold(int gold)
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
        isGameOver = true;
        Debug.Log("Game Over");
        SceneManager.LoadScene("Menu");
    }
}
