using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineTPFollowController : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    private float cameraSideTarget = 1;
    public float CameraSideTarget => cameraSideTarget;

    [SerializeField] private float cameraSideLerpFactor;

    public void SetShoulderOffset(float val)
    {
        cameraSideTarget = val;
    }

    private void FixedUpdate()
    {
        var tpFollow = cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        float current = tpFollow.CameraSide;
        tpFollow.CameraSide = Mathf.Lerp(current, cameraSideTarget, cameraSideLerpFactor * Time.fixedDeltaTime);
        
    }
}
