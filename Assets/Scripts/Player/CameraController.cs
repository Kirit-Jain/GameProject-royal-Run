using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedEffect;
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5f;

    CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutinte(speedAmount));

        if (speedAmount > 0 && !speedEffect.isPlaying)
        {
            speedEffect.Play();
        }
        else if (speedAmount < 0 && speedEffect.isPlaying)
        {
            speedEffect.Stop();
        }
        
    }

    IEnumerator ChangeFOVRoutinte(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + speedAmount * zoomSpeedModifier, minFOV, maxFOV);

        float timeElapsed = 0f;

        while (timeElapsed < zoomDuration)
        {
            float t = timeElapsed / zoomDuration;
            timeElapsed += Time.deltaTime;

            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
