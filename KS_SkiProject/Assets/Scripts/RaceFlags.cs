using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaceFlags : MonoBehaviour
{
    private enum FlagType
    {
        Start,
        Penalty,
        Boost,
        Finish
    }
    
    [SerializeField] private FlagType flagType = FlagType.Penalty;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerFlagEvent();
        }
    }

    private void TriggerFlagEvent()
    {
        switch (flagType)
        {
            case FlagType.Start:
                GameManager.CallOnRaceStart();
                break;
            case FlagType.Penalty:
                GameManager.CallOnTimePenalty();
                break;
            case FlagType.Boost:
                PlayerEvents.CallOnBoost();
                break;
            case FlagType.Finish:
                GameManager.CallOnRaceEnd();
                break;
        }
    }
}
