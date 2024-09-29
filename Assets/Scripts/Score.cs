using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoretext;// ������ �� ��������� UI �������
    private float elapsedTime;
    private int score;
    private int highscore;
    private bool startgame = false;
    public GameObject taptostart;

    // Start ���������� ��� ������������� �����
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        elapsedTime = 0f;
        score = 0;
        UpdateScoreText();
        UpdateHighScoreText();
    }

    // Update ���������� ������ ����
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (!startgame)
            {
                Destroy(taptostart);
                // ���� ���� �� ��������, ��������� �
                startgame = true;
                elapsedTime = 0f; // ���������� ����� ��� ������ ����
            }
        }
        if(startgame)
        {
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

    // ���������� ������ � UI
    void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    void UpdateHighScoreText()
    {
        highscoretext.text = "HIGHSCORE: " + highscore.ToString();
    }
}
