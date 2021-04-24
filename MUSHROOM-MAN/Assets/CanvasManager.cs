using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text playerHealthText;
    public TMP_Text playerStatusText;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;
    public GameObject instructionPanel;

    public int timer = 120;
    public MovementController player;



    private void Awake()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownOne());
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = player.playerHealth.ToString();
        playerStatusText.text = player.playerStatus;
        if (player.playerStatus == "DEAD")
        {
            losePanel.SetActive(true);
        }
        if (player.playerHealth <= 0)
        {
            losePanel.SetActive(true);
        }
        if (player.playerStatus == "WIN")
        {
            winPanel.SetActive(true);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        instructionPanel.SetActive(false);
    }

    public void Reload()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator CountDownOne()
    {
        timer--;
        if (timer <= 0)
        {
            losePanel.SetActive(true);
        }
        timerText.text = timer.ToString();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(CountDownOne());
    }
}
