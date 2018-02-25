using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public const int COIN_SCORE_AMOUNT = 1;
    public static GameManager Instance { set; get; }

    private bool isGameStarted = false;
    private PlayerMotor motor;

    
    public Animator gameCanvas;
    public bool isDead { set; get;}
    public Text scoreText, coinText, modifierText;
    private float score, coinScore, modifierScore;
    private int lastScore;

    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    public Animator mainMenuAnim;

    public Text hiscoreText;

    public int deathCount;
    bool deathcountstart;
    // Use this for initialization
    private void Awake()
    {
        Instance = this;
        modifierText.text = "Multiplier: x" + modifierScore.ToString("0.0");
        coinText.text = "Cheese: " + coinScore.ToString("0");
        scoreText.text = "Score: " + score.ToString("0");
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        hiscoreText.text = "High Score: " + PlayerPrefs.GetInt("Hiscore").ToString("0");
        
     
    }

    private void Update()
    {
        if(MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            motor.StartRunning();
            FindObjectOfType<cameraMotor>().isMoving = true;
            gameCanvas.SetTrigger("Show");
            mainMenuAnim.SetTrigger("BeginPlay");
        }

        if(isGameStarted && !isDead)
        {
            
            score += (Time.deltaTime * modifierScore);
            
            if( lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = "Score: " + score.ToString("0");
            }
        }
        else if(isGameStarted && isDead && deathcountstart)
        {
            if (deathCount >= 6 && coinScore > 0)
            {
                score += 10;
                coinScore--;
                deathCount = 0;
                deathText();
                // Invoke("deathText)", 0);
            }
            else
                deathCount++;
        }
    }

  

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "Multiplier: x" + modifierScore.ToString("0.0");
    }

    public void GetCoin()
    {
        coinScore += COIN_SCORE_AMOUNT;
        coinText.text = "Cheese: " + coinScore.ToString("0");
        //scoreText.text = score.ToString("0");
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BuildtestScene");
    }

    public void OnDeath()
    {
        isDead = true;
       
        gameCanvas.SetTrigger("Hide");
        deathMenuAnim.SetTrigger("Dead");
        deathText();
        Invoke("DeathAnim", 2);

        //Check if it is a highscore
        if(score > PlayerPrefs.GetInt("Hiscore"))
        {
            float s = score;
            if (s % 1 == 0)
                s += 1;
            PlayerPrefs.SetInt("Hiscore", (int)s);
        }
    }

    public void deathText()
    {
        deadScoreText.text = "Score: " + score.ToString("0");
        deadCoinText.text = "Cheese: " + coinScore.ToString("0");
    }

    public void DeathAnim()
    {
        deathcountstart = true;
    }
}
