using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int waveNumber = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void NextWave()
    {
        this.waveNumber++;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
