using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Image overlay;


    private void Start()
    {
        overlay.CrossFadeAlpha(0f, 1f, true);
        gameOverUI.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.raceEnd += EnableGameOverUI;
        GameManager.gameQuit += Quit;
    }
    
    private void OnDisable()
    {
        GameManager.raceEnd -= EnableGameOverUI;
        GameManager.gameQuit -= Quit;
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadSceneWithCrossFade(1f, SceneManager.GetActiveScene().buildIndex));
    }

    public void GoToNextLevel()
    {
        StartCoroutine(LoadSceneWithCrossFade(1f, SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void QuitGame()
    {
        GameManager.CallOnGameQuit();
    }

    private IEnumerator LoadSceneWithCrossFade(float fadeTime, int sceneIndex)
    {
        overlay.CrossFadeAlpha(1f, fadeTime, true);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneIndex);
    }

    private void Quit()
    {
        StartCoroutine(QuitCoroutine(1));
    }

    private IEnumerator QuitCoroutine(float fadeTime)
    {
        overlay.CrossFadeAlpha(1f, fadeTime, true);
        yield return new WaitForSeconds(fadeTime);
        Application.Quit();
        print("Quit that shit.");
    }

    private void EnableGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
}
