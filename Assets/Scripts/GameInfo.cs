using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public string FactionChosen { get; private set; }

    private GameUI gameUI;

    public void IncreaseScore(int gainAmount)
    {
        Score += gainAmount;
        gameUI.UpdateScoreText(Score);
    }

    private void Awake()
    {
        Score = 0;
        HighScore = 0;
        FactionChosen = "";

        gameUI = FindObjectOfType<GameUI>();
    }
}
