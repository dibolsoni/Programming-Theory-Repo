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
    public int startGold = 10;
    public int time;
    public int timePerWave = 20;
    public bool isGameOver = false;
    // ENCAPSULATION
    public PlayerManager playerState { get { return PlayerManager.Instance; } }


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

    public void removeGold(int gold)
    {
        playerState.decreaseGold(gold);
    }

    public void addGold(int gold)
    {
        playerState.increaseGold(gold);
    }   

    public void removeLife(int life = 1)
    {
        playerState.decreaseLife(life);
        valitateGameOver();
    }

    public void valitateGameOver()
    {

        if (isGameOver || playerState.lifeRemaining <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        StopAllCoroutines();
        Debug.Log("Game Over");
        PlayerManager.Instance.bestScore = waveNumber;
        SceneManager.LoadScene("Menu");
    }
}
