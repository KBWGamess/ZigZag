using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoretext;
    private float elapsedTime;
    private int score;
    private int highscore;
    private bool startgame = false;
    public GameObject taptostart;
    public GameObject startButton;
    public GameObject storeButton;
    public GameObject crystalsText;

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        elapsedTime = 0f;
        score = 0;
        UpdateScoreText();
        UpdateHighScoreText();
    }

    void Update()
    {
        ScoreStart();        
    }

    // Обновление текста в UI
    void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    void UpdateHighScoreText()
    {
        highscoretext.text = "HIGHSCORE: " + highscore.ToString();
    }

    void ScoreStart()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (!startgame)
            { 
                
            }
        }
        if (startgame)
        {
            Destroy(taptostart);
            Destroy(startButton);
            storeButton.SetActive(false);
            crystalsText.SetActive(false);
            elapsedTime += Time.deltaTime * 5;
            score = Mathf.FloorToInt(elapsedTime);
            UpdateScoreText();

            if (highscore < score)
            {
                highscore = score;
                PlayerPrefs.SetInt("highscore", highscore);
                UpdateHighScoreText();
            }
        }
    }

    public void PlayGame()
    {
        startgame = true;
        Debug.Log(startgame);
    }

    public void StopScore()
    {
        elapsedTime = score;
    }
}
