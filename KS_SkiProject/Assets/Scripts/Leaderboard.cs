using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<float> scores = new();


    public void AddScore(float score)
    {
        scores.Add(score);    
        scores.Sort();
        SaveTimes();
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
        scores.Clear();
        LoadTimes();
    }

    private void DeleteData()
    {
        scores.Clear();
        PlayerPrefs.DeleteAll();
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
            PlayerPrefs.SetFloat($"time {i}", score);
            i++;
            if (i > 5)
                break;
        }
        PlayerPrefs.Save();
    }

    private void LoadTimes()
    {
        //scores = new List<float>();
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.GetFloat($"time {i}") != 0f)
                scores.Add(PlayerPrefs.GetFloat($"time {i}"));
            else
                break;
        }
    }
}
