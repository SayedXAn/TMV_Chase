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
    public GameObject[] healthBar;

    [Header("Texts and InputFields")]
    public TMP_Text timerText;
    public TMP_Text finishText;

    [Header("Variables")]
    public int healthCount = 2;
    public int gameTime = 50;
    public bool gameOn = false;


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
        menuPanel.SetActive(false);
        gameUI.SetActive(true);
        environmentRoot.SetActive(true);
        gameOn = true;
    }
    public void PlayAgainButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
