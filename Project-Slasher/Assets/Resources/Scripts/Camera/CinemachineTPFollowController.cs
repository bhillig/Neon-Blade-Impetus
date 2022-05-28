using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
public class CinemachineTPFollowController : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    private float cameraSideTarget = 1;
    public float CameraSideTarget => cameraSideTarget;

    [SerializeField] private float cameraSideLerpFactor;

    private SortedDictionary<int, float> targets = new SortedDictionary<int, float>();

    public void SetShoulderOffset(float val, int layer)
    {
        if(!targets.ContainsKey(layer))
            targets.Add(layer, val);
        else
            targets[layer] = val;
    }

    public void RemoveKey(int layer)
    {
        targets.Remove(layer);
    }


    private void FixedUpdate()
    {
        if (targets.Count > 0)
            cameraSideTarget = targets.Values.Last();
        else
            cameraSideTarget = 1;
        Debug.Log(cameraSideTarget);
        var tpFollow = cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        float current = tpFollow.CameraSide;
        tpFollow.CameraSide = Mathf.Lerp(current, cameraSideTarget, cameraSideLerpFactor * Time.fixedDeltaTime);   
    }
}
