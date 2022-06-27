using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private GameObject titleMenu;
    private GameObject chooseMenu;
    private AudioPlayer audioPlayer;

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
        chooseMenu.SetActive(false);
    }
}
