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
        StartCoroutine(LoadScene(mainMenuSceneIndex));
    }

    public void LoadGameScene()
    {
        FindObjectOfType<GameInfo>().OnGameRestart();
        StartCoroutine(LoadScene(gameSceneIndex));
    }

    public void LoadRetryMenuScene()
    {
        StartCoroutine(LoadScene(retryMenuSceneIndex));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        FindObjectOfType<AudioPlayer>().PlayClick();
        const float loadSceneTimeDelay = 0.25f;
        yield return new WaitForSeconds(loadSceneTimeDelay);
        SceneManager.LoadScene(sceneIndex);
    }
}
