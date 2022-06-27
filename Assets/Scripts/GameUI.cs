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

    private GameInfo gameInfo;
    private GameObject blueSpaceBackground;
    private GameObject redSpaceBackground;

    public static string FormatNumber(int amount)
    {
        if (amount >= 1000)
        {
            char[] textArray = amount.ToString().ToCharArray();
            List<char> formattedScore = new List<char>();
            Array.Reverse(textArray);

            for (int i = 0; i < textArray.Length; i++)
            {
                formattedScore.Add(textArray[i]);
                if ((i + 1) % 3 == 0 && i + 1 != textArray.Length)
                    formattedScore.Add(',');
            }

            formattedScore.Reverse();
            return string.Join("", formattedScore);
        }
        else
            return amount.ToString();
    }

    public void UpdateScoreText(int scoreAmount)
    {
        string formattedValue = FormatNumber(scoreAmount);
        scoreText.text = string.Format("Score: {0}", formattedValue);
    }

    public void UpdateDifficultyText(int difficultyLevel)
    {
        if (difficultyLevel == enemySpawner.GetMaxDifficultyLevel())
            difficultyText.text = "Difficulty: Max";
        else
            difficultyText.text = string.Format("Difficulty: {0}", difficultyLevel);
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

    public void UpdateLevelSlider(float fillValue, float maxValue)
    {
        levelSlider.maxValue = maxValue;
        levelSlider.value = fillValue;
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

        gameInfo = FindObjectOfType<GameInfo>();
        blueSpaceBackground = GameObject.Find("BlueSpaceTileMap");
        redSpaceBackground = GameObject.Find("RedSpaceTileMap");

        blueSpaceBackground.SetActive(false);
        redSpaceBackground.SetActive(false);
    }

    private void Start()
    {
        if (gameInfo.FactionChosen == "RED")
            redSpaceBackground.SetActive(true);
        else
            blueSpaceBackground.SetActive(true);
    }
}
