using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private float stunRecoilForce = 8f;
    private float stunTime;
    public float StunTime { get => stunTime; }
    private Rigidbody rb;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        PlayerEvents.onHit += GetHit;
    }

    private void OnDisable()
    {
        PlayerEvents.onHit -= GetHit;
    }

    private void GetHit()
    {
        GetComponent<Animator>().Play("Stop");
        StartCoroutine(Stun());
    }
    
    private IEnumerator Stun(float time = 1f)
    {
        stunTime = time;
        rb.AddForce(-transform.forward * stunRecoilForce, ForceMode.Impulse);

        while (stunTime > 0f)
        {
            stunTime -= Time.deltaTime;
            yield return null;
        }
    }
}
