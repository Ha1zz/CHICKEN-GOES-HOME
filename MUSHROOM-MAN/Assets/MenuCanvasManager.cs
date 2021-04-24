using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvasManager : MonoBehaviour
{
    public GameObject creditPanel;
    public GameObject instructionPanel;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return()
    {
        instructionPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void ShowInstruction()
    {
        instructionPanel.SetActive(true);
    }

    public void ShowCredit()
    {
        creditPanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
