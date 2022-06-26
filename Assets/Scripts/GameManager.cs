using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int mainMenuSceneIndex = 0;
    private const int gameSceneIndex = 1;
    private const int retryMenuSceneIndex = 2;

    public void LoadMainMenuScene()
    {
        LoadScene(mainMenuSceneIndex);
    }

    public void LoadGameScene()
    {
        LoadScene(gameSceneIndex);
    }

    public void LoadRetryMenuScene()
    {
        LoadScene(retryMenuSceneIndex);
    }

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
