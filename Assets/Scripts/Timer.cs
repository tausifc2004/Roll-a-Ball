using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject lostText;
    public GameObject retryText;
    [SerializeField] private GameObject gameOverMenu;

    public float targetTime = 45.0f;


    // Update is called once per frame
    void Update()
    {
        timerText.text = "Timer: " + targetTime.ToString("N0");
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            gameOver();
        }
    }

    void gameOver()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        timerText.text = "";
        lostText.SetActive(true);
        retryText.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
