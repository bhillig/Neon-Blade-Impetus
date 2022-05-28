using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraCollisionPanning : MonoBehaviour
{
    public CinemachineTPFollowController TPFollowController;
    public CinemachineVirtualCamera vCam;
    public Camera mainCam;
    public LayerMask collisionMask;
    private Cinemachine3rdPersonFollow follow;
    public float radius;
    public float leftPan;

    private void Awake()
    {
        follow = vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    private void FixedUpdate()
    {
        Vector3 right = mainCam.transform.right;
        Vector3 pos  = mainCam.transform.position;
        float pan = follow.CameraSide;
        float dist = follow.ShoulderOffset.x;
        Vector3 leftPoint = -pan * right * 2 * dist + pos;
        Vector3 rightPoint = (1 - pan) * right * 2 * dist + pos;
        Debug.DrawLine(leftPoint, rightPoint, Color.green);
        var lefthits = Physics.OverlapSphere(leftPoint,radius,collisionMask);
        var righthits = Physics.OverlapSphere(rightPoint,radius,collisionMask);

        bool leftHit = lefthits.Length > 0;
        bool rightHit = righthits.Length > 0;

        if(leftHit)
            TPFollowController.SetShoulderOffset(1f,-1);
        else if(rightHit)
            TPFollowController.SetShoulderOffset(leftPan,1);
        else
            TPFollowController.SetShoulderOffset(1f,1);
    }
}
