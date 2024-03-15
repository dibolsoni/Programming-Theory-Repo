using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject endPanel;
    public TextMeshProUGUI bestScoreText;
    // ENCAPSULATION
    private bool isGameOver { get { return GameManager.Instance.isGameOver; } }


    public void Start()
    {
        if (isGameOver)
        {
            GameOver();
        }
        else
        {
            StartMenu();
        }
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        PlayerManager.Instance.Reset();
        GameManager.Instance.Reset();
    }

    public void StartMenu()
    {   
        endPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        UpdateBestScore();
        startPanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void UpdateBestScore()
    {
        int bestScore = PlayerManager.Instance.bestScore | 0;
        bestScoreText.text = "Your best score was " + bestScore + " waves!";
    }
}
