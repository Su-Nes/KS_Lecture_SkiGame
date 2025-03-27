using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;
    
    private void OnEnable()
    {
        PlayerEvents.onHit += PlayHitSound;
    }
    
    private void OnDisable()
    {
        PlayerEvents.onHit -= PlayHitSound;
    }

    private void PlayHitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(hitSound);
    }
}
