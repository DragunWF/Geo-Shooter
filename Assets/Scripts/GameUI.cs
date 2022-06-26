using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI difficultyText;
    private EnemySpawner enemySpawner;
    private AudioPlayer audioPlayer;

    public void UpdateScoreText(int scoreAmount)
    {
        if (scoreAmount >= 1000)
        {
            char[] scoreTextArray = scoreAmount.ToString().ToCharArray();
            List<char> formattedScore = new List<char>();
            Array.Reverse(scoreTextArray);

            for (int i = 0; i < scoreTextArray.Length; i++)
            {
                formattedScore.Add(scoreTextArray[i]);
                if ((i + 1) % 3 == 0 && i + 1 != scoreTextArray.Length)
                    formattedScore.Add(',');
            }

            formattedScore.Reverse();
            scoreText.text = string.Join("", formattedScore);
        }
        else
            scoreText.text = scoreAmount.ToString();
    }

    public void UpdateDifficultyText(int difficultyLevel)
    {
        if (difficultyLevel == enemySpawner.GetMaxDifficultyLevel())
        {
            difficultyText.text = "Max";
            // Add sound effect
        }
        else
        {
            difficultyText.text = difficultyLevel.ToString();
            // Add sound effect
        }
    }

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        difficultyText = GameObject.Find("DifficultyText").GetComponent<TextMeshProUGUI>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
}
