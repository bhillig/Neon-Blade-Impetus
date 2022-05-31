using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
public class CinemachineTPFollowController : MonoBehaviour
{
    [Header("Dependencies")]
    public CinemachineVirtualCamera cam;
    public AnimationCurve lerpCurve;

    [Header("Values")]
    [SerializeField] private float lerpTime;
    private float lerpTimer;

    // Private fields
    private float cameraSideTarget = 1;
    private float newVal = 1;
    private float prevTarget;

    private float multiplier = 1f;
    private Cinemachine3rdPersonFollow tpFollow;

    // Default values
    private Vector3 defaultDamp;
    private float defaultDistance;

    public void SetShoulderOffset(float val)
    {
        newVal = val;
    }

    public void SetLerpTimeMultiplier(float val)
    {
        multiplier = val;
    }

    public void SetDampZ(float damp)
    {
        tpFollow.Damping.z = damp;
    }
    public void SetDampY(float damp)
    {
        tpFollow.Damping.y = damp;
    }

    public void SetDamp(Vector3 damp)
    {
        tpFollow.Damping = damp;
    }

    public void ResetDamp()
    {
        tpFollow.Damping = defaultDamp;
    }

    public void SetDistance(float val)
    {
        tpFollow.CameraDistance = val;
    }

    public void ResetDistance()
    {
        tpFollow.CameraDistance = defaultDistance;
    }

    private void Awake()
    {
        tpFollow = cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        defaultDamp = tpFollow.Damping;
        defaultDistance = tpFollow.CameraDistance;
    }

    private void FixedUpdate()
    { 
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
