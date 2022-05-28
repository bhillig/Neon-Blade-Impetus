using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class CameraFOVController : MonoBehaviour
{
    public Rigidbody body;
    public float minVel;
    public float maxVel;
    public float maxFOV;
    public float minFOV;
    public AnimationCurve fovCurve;
    public AnimationCurve lensDistortCurve;
    public CinemachineVirtualCamera cam;
    public float lerpSpeed;

    private Volume postProcessVolume;

    private void Start()
    {
        postProcessVolume = GetComponent<Volume>();
    }

    private void LateUpdate()
    {
        float currentFOV = cam.m_Lens.FieldOfView;
        float t = Mathf.InverseLerp(minVel,maxVel,body.velocity.XZMag());
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, fovCurve.Evaluate(t));
        cam.m_Lens.FieldOfView = Mathf.MoveTowards(currentFOV, targetFOV, lerpSpeed);
        float lenseDistort = Mathf.Lerp(0, -1, lensDistortCurve.Evaluate(t));
        postProcessVolume.sharedProfile.TryGet(out LensDistortion lenseDistortProfile);
        lenseDistortProfile.intensity.value = lenseDistort;
    }

}
