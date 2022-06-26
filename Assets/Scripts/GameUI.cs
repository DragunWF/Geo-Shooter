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

    public void UpdateDifficultyText(int difficultyLevel)
    {
        if (difficultyLevel >= enemySpawner.GetMaxDifficultyLevel())
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

    public void UpdateScoreText(int scoreAmount)
    {
        char[] scoreTextArray = scoreAmount.ToString().ToCharArray();
        List<char> formattedScore = new List<char>();
        Array.Reverse(scoreTextArray);

        for (int i = 0; i < scoreTextArray.Length; i++)
        {
            if (i + 1 % 3 == 0)
                formattedScore.Add(',');
            formattedScore.Add(scoreTextArray[i]);
        }

        formattedScore.Reverse();
        scoreText.text = string.Join("", formattedScore);
    }

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        difficultyText = GameObject.Find("DifficultyText").GetComponent<TextMeshProUGUI>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
}
