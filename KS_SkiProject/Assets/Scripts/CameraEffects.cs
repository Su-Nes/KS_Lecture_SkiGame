using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] private float screenShakeDuration = 0.5f;
    private CinemachineVirtualCamera vCam;
    
    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        PlayerEvents.onHit += EnableScreenShake;
    }
    
    private void OnDisable()
    {
        PlayerEvents.onHit -= EnableScreenShake;
    }

    private void EnableScreenShake()
    {
        StartCoroutine(ScreenShake());
    }

    private IEnumerator ScreenShake()
    {
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1f;
        yield return new WaitForSeconds(screenShakeDuration);
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }
}
