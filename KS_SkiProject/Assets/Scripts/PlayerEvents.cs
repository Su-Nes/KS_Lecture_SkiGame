using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public delegate void OnPlayerHit();
    public static event OnPlayerHit onHit;

    public static void CallOnPlayerHit()
    {
        onHit?.Invoke();
    }
}
