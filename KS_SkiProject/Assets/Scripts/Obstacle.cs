using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected PlayerControl player;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerCollision(other.gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCollision(other.gameObject);
        }
    }

    protected virtual void PlayerCollision(GameObject otherObj)
    {
        player = otherObj.gameObject.GetComponent<PlayerControl>();
        
        PlayerEvents.CallOnPlayerHit();
    }
}
