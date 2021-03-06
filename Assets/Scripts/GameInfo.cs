using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int Score { get; private set; }
    public int HighScore { get; private set; }
    public string FactionChosen { get; private set; }
    public bool ReachedNewHighScore { get; private set; }

    private GameUI gameUI;
    private RetryMenuUI retryMenuUI;
    static private GameInfo instance;

    public void ChooseRedFaction() { FactionChosen = "RED"; }
    public void ChooseBlueFaction() { FactionChosen = "BLUE"; }

    public void RedefineObjects()
    {
        gameUI = FindObjectOfType<GameUI>();
        retryMenuUI = FindObjectOfType<RetryMenuUI>();
    }

    public void IncreaseScore(int gainAmount)
    {
        Score += gainAmount;
        gameUI.UpdateScoreText(Score);
    }

    public void SaveScore()
    {
        if (Score > HighScore)
        {
            ReachedNewHighScore = true;
            HighScore = Score;
        }
    }

    public void OnGameRestart()
    {
        Score = 0;
        ReachedNewHighScore = false;
    }

    private void Awake()
    {
        Score = 0;
        HighScore = 0;
        FactionChosen = "";
        ReachedNewHighScore = false;

        RedefineObjects();
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
