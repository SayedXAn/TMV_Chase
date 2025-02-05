using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject menuPanel;
    public GameObject gameUI;
    public GameObject finishPanel;
    public GameObject environmentRoot;
    public Camera mainCamera;
    public GameObject[] healthBar;

    [Header("Texts and InputFields")]
    public TMP_Text timerText;
    public TMP_Text finishText;
    public TMP_Text menuNotification;
    public TMP_InputField nameIF;

    [Header("Variables")]
    public int healthCount = 2;
    public int gameTime = 50;
    public bool gameOn = false;
    private int playerPos = -2;

    [Header("Scripts")]
    public LB_Manager lbman;

    void Start()
    {
        DOTween.Init();
        menuPanel.SetActive(true);
        gameUI.SetActive(false);
        environmentRoot.SetActive(false);
        finishPanel.SetActive(false);
        gameOn = false;
    }



    IEnumerator GameTimerCountDown()
    {
        yield return new WaitForSeconds(1f);
        gameTime--;
        if(gameTime < 0)
        {
            gameOn = false;
            GameOver(true);
            StopCoroutine(GameTimerCountDown());
        }
        else
        {
            timerText.text = gameTime.ToString();
            StartCoroutine(GameTimerCountDown());
        }
    }
    public bool OnnoTrackerNise()
    {
        //pida halare
        healthBar[healthCount].GetComponent<Image>().DOFade(0f, 0.5f);
        healthCount--;
        if(healthCount < 0)
        {
            GameOver(true);
            return false;
        }
        return true;
    }
    public void GameWin()
    {
        gameOn = false;             
        menuPanel.SetActive(false);
        gameUI.SetActive(false);
        environmentRoot.SetActive(false);
        finishPanel.SetActive(true);
        finishText.text = "You Win!\nPlay Again";
    }

    public void GameOver(bool timeOver)
    {
        gameOn = false;
        menuPanel.SetActive(false);
        gameUI.SetActive(false);
        environmentRoot.SetActive(false);
        finishPanel.SetActive(true);
        if (timeOver)
        {
            finishText.text = "Time Over!\nTry Again";
        }
        else
        {
            finishText.text = "Try to Collect the best tracker\nAvoid obstacles\nChase your stolen car";
        }
        
        
    }

    public void StartButton()
    {
        if(nameIF.text == "")
        {
            menuNotification.text = "Enter your name before proceeding";
            return;
        }
        menuPanel.SetActive(false);
        gameUI.SetActive(true);
        environmentRoot.SetActive(true);
        mainCamera.clearFlags = CameraClearFlags.Skybox;
        gameOn = true;
        StartCoroutine(GameTimerCountDown());
    }
    public void PlayAgainButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SetPlayerPos(int pos)
    {
        playerPos = pos;
    }
    IEnumerator ShowPlayerPositon()
    {
        yield return new WaitForSeconds(0.1f);
        if (playerPos == -2)
        {
            StartCoroutine(ShowPlayerPositon());
        }
        else
        {
            finishText.text = finishText.text + "your position: " + playerPos.ToString();
            StopCoroutine(ShowPlayerPositon());
        }
    }

    public void UpdateScoreToLeaderboard()
    {
        lbman.SetEntry("Sayyyeeed", 123);
    }

    public void ShowLeaderboard()
    {
        lbman.GenerateLeaderboard();
    }
}
