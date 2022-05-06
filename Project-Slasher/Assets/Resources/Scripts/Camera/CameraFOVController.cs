using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraFOVController : MonoBehaviour
{
    public Rigidbody body;
    public float minVel;
    public float maxVel;
    public float maxFOV;
    public float minFOV;
    public AnimationCurve fovCurve;
    public CinemachineVirtualCamera cam;
    public float lerpSpeed;

    private void LateUpdate()
    {
        float currentFOV = cam.m_Lens.FieldOfView;
        float t = Mathf.InverseLerp(minVel,maxVel,body.velocity.XZMag());
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, fovCurve.Evaluate(t));
        cam.m_Lens.FieldOfView = Mathf.MoveTowards(currentFOV, targetFOV, lerpSpeed);
    }

}
