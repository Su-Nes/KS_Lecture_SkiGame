using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<float> scores = new();


    private void Start()
    {
        LoadTimes();
    }

    public void AddScore(float score)
    {
        scores.Add(score);    
        scores.Sort();
        SaveTimes();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += ClearTimes;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= ClearTimes;
    }

    private void ClearTimes(Scene scene, Scene nextScene)
    {
        scores.Clear();
        
    }

    private void SaveTimes()
    {
        int i = 0;
        foreach (int score in scores)
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
        scores = new List<float>();
        for (int i = 0; i < 5; i++)
        {
            if(PlayerPrefs.HasKey($"time {i}"))
                scores.Add(PlayerPrefs.GetFloat($"time {i}"));
        }
    }
}
