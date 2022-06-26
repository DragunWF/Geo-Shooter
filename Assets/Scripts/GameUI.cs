using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI difficultyText;
    private TextMeshProUGUI healthText;

    private TextMeshProUGUI levelText;
    private Slider levelSlider;

    private EnemySpawner enemySpawner;
    private AudioPlayer audioPlayer;
    private Player player;

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
            scoreText.text = string.Format("Score: {0}", string.Join("", formattedScore));
        }
        else
            scoreText.text = string.Format("Score: {0}", scoreAmount);
    }

    public void UpdateDifficultyText(int difficultyLevel)
    {
        if (difficultyLevel == enemySpawner.GetMaxDifficultyLevel())
        {
            difficultyText.text = "Difficulty: Max";
            // Add sound effect
        }
        else
        {
            difficultyText.text = string.Format("Difficulty: {0}", difficultyLevel);
            // Add sound effect
        }
    }

    public void UpdateHealthText(int healthAmount)
    {
        string formattedHealthValue = healthAmount > 0 ? healthAmount.ToString() : "Dead";
        healthText.text = string.Format("Health: {0}", formattedHealthValue);
    }

    public void UpdateLevelText(int level)
    {
        string formattedLevelValue = level != player.GetMaxLevel() ? level.ToString() : "Maxed";
        levelText.text = string.Format("Level {0}", formattedLevelValue);
    }

    public void UpdateLevelSlider(int experiencePoints)
    {

    }

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        difficultyText = GameObject.Find("DifficultyText").GetComponent<TextMeshProUGUI>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();

        levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
        levelSlider = GameObject.Find("LevelSlider").GetComponent<Slider>();

        enemySpawner = FindObjectOfType<EnemySpawner>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        player = FindObjectOfType<Player>();
    }
}
