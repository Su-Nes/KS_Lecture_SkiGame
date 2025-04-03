using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    private float timer, timePenalty;
    private bool raceStarted;
    [SerializeField] private Leaderboard leaderboard;
    
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
