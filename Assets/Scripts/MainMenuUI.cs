using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private GameObject titleMenu;
    private GameObject chooseMenu;
    private AudioPlayer audioPlayer;
    private GameInfo gameInfo;

    public void OnRedButtonClick()
    {
        gameInfo.ChooseRedFaction();
    }

    public void OnBlueButtonClick()
    {
        gameInfo.ChooseBlueFaction();
    }

    public void OnPlayButtonClick()
    {
        audioPlayer.PlayClick();
        titleMenu.SetActive(false);
        chooseMenu.SetActive(true);
    }

    private void Awake()
    {
        titleMenu = GameObject.Find("TitleMenu");
        chooseMenu = GameObject.Find("ChooseMenu");
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameInfo = FindObjectOfType<GameInfo>();
        chooseMenu.SetActive(false);
    }
}
