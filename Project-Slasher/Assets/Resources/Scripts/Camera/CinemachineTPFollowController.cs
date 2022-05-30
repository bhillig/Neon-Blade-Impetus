using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
public class CinemachineTPFollowController : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    private float cameraSideTarget = 1;
    private float newVal = 1;
    private float prevTarget;

    public AnimationCurve lerpCurve;

    [SerializeField] private float lerpTime;
    private float lerpTimer;

    private float multiplier = 1f;

    public void SetShoulderOffset(float val)
    {
        newVal = val;
    }

    public void SetLerpTimeMultiplier(float val)
    {
        multiplier = val;
    }

    private void FixedUpdate()
    {
        var tpFollow = cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        lerpTimer += Time.fixedDeltaTime;
        if (newVal != cameraSideTarget)
        {
            prevTarget = tpFollow.CameraSide;
            cameraSideTarget = newVal;
            lerpTimer = 0f;
        }
        //Debug.Log(cameraSideTarget);
        float t = lerpTimer / (lerpTime * multiplier);
        tpFollow.CameraSide = Mathf.Lerp(prevTarget, cameraSideTarget, lerpCurve.Evaluate(t));
    }
}
