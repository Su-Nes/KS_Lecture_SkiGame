using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    private float timer;
    [SerializeField] private float timePenalty = 1f;
    private bool raceStarted;
    
    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private TMP_Text timerText;
    
    private void OnEnable()
    {
        GameManager.raceStart += StartTimer;
        GameManager.raceEnd += StopTimer;
        GameManager.timePenalty += TimePenalty;
    }
    
    private void OnDisable()
    {
        GameManager.raceStart -= StartTimer;
        GameManager.raceEnd -= StopTimer;
        GameManager.timePenalty -= TimePenalty;
    }

    private void Update()
    {
        if (!raceStarted)
            return;
        
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F2");
    }

    private void StartTimer()
    {
        raceStarted = true;
        timer = 0f;
    }
    
    private void StopTimer()
    {
        raceStarted = false;
        GameData.Instance.racesCompleted++;
        leaderboard.AddScore(timer);
        print($"Finished in {timer} seconds!");
    }

    private void TimePenalty()
    {
        timer += timePenalty;
        print($"1 second penalty! {timer} seconds");
    }
}
