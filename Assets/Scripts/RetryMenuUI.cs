using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RetryMenuUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI newHighScoreText;
    private GameInfo gameInfo;

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        newHighScoreText = GameObject.Find("NewHighScoreText").GetComponent<TextMeshProUGUI>();
        gameInfo = FindObjectOfType<GameInfo>();
    }

    private void Start()
    {
        GameObject newHighScoreGameObject = newHighScoreText.gameObject;
        newHighScoreGameObject.SetActive(gameInfo.ReachedNewHighScore);

        scoreText.text = string.Format("Score: {0}", GameUI.FormatNumber(gameInfo.Score));
        highScoreText.text = string.Format("High Score: {0}", GameUI.FormatNumber(gameInfo.HighScore));
    }
}
