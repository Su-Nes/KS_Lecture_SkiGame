using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    private List<float> scores = new();

    [SerializeField] private Transform scoreTextParent;
    [SerializeField] private TMP_Text leaderboardTitle;
    [SerializeField] private GameObject scoreDisplayObj, leaderboardPanel;
    

    public void AddScore(float score)
    {
        scores.Add(score);    
        scores.Sort();
        SaveTimes();
        leaderboardPanel.SetActive(true);
        DisplayLeaderboard();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += RefreshTimes;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= RefreshTimes;
    }

    private void RefreshTimes(Scene scene, Scene nextScene)
    {
        leaderboardPanel.SetActive(false);
        scores.Clear();
        LoadTimes();
    }

    public void DeleteData()
    {
        scores.Clear();
        PlayerPrefs.DeleteAll();
        DisplayLeaderboard();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DeleteData();
        }
    }

    private void SaveTimes()
    {
        int i = 0;
        foreach (float score in scores)
        {
            PlayerPrefs.SetFloat($"time {i}; scene {SceneManager.GetActiveScene().name}", score);
            i++;
            if (i > 5)
                break;
        }
        PlayerPrefs.Save();
    }

    private void LoadTimes()
    {
        scores.Clear();
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.GetFloat($"time {i}; scene {SceneManager.GetActiveScene().name}") != 0f)
                scores.Add(PlayerPrefs.GetFloat($"time {i}; scene {SceneManager.GetActiveScene().name}"));
            else
                break;
        }
    }

    public void DisplayLeaderboard()
    {
        foreach (Transform child in scoreTextParent)
        {
            Destroy(child.gameObject);
        }

        leaderboardTitle.text = $"{SceneManager.GetActiveScene().name} scores";

        int scoreNum = 1;
        foreach (float score in scores)
        {
            TMP_Text scoreText = Instantiate(scoreDisplayObj, Vector3.zero, Quaternion.identity, scoreTextParent).GetComponent<TMP_Text>();
            scoreText.text = $"{scoreNum}. {score:F2}";
            scoreNum++;

            if (scoreNum > 5)
                break;
        }
    }
}
