using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void RaceEvent();
    public static event RaceEvent raceStart, raceEnd, timePenalty;

    public static void CallOnRaceStart()
    {
        raceStart?.Invoke();
    }

    public static void CallOnRaceEnd()
    {
        raceEnd?.Invoke();
    }

    public static void CallOnTimePenalty()
    {
        timePenalty?.Invoke();
    }
}
