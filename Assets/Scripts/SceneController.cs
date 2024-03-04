using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject endPanel;

    private void Awake()
    {
        bool isGameOver = GameManager.Instance.isGameOver;
        startPanel.SetActive(!isGameOver);
        endPanel.SetActive(isGameOver);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void StartMenu()
    {
        startPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        endPanel.SetActive(true);
    }
}
