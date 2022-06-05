using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/// <summary>
/// This does rely on cinemachine to control the actual camera
/// This class simply rotates a target transform, which cinemachine is intended to track
/// </summary>
public class ThirdPersonCameraTargetController : MonoBehaviour
{
    // Serialized fields
    public Transform TPTarget;
    public Transform toFollow;
    public Vector3 offset;

    // Public fields
    public InputInfo inputInfo;
    public float Sensitivity;
    public float Damping;
    public float VerticalRangeUp;
    public float VerticalRangeDown;

    private Vector2 mouseDelta => inputInfo.mouseDelta;
    public float yRotation { get; private set; }
    public float xRotation { get; private set; }

    public Quaternion rotation { get { return Quaternion.Euler(xRotation, yRotation, 0); } }

    private void Awake()
    {
        LockMouse(true);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        TPTarget.position = toFollow.position + offset;
        CameraRotation();
    }

    private void FixedUpdate()
    {
        TPTarget.position = toFollow.position + offset;
    }

    private void CameraRotation()
    {
        yRotation += Time.unscaledDeltaTime * Sensitivity * mouseDelta.x;
        xRotation -= Time.unscaledDeltaTime * Sensitivity * mouseDelta.y;
        // Clamping vertical looking       
        LerpCameraToValues(1);
    }

    private void LerpCameraToValues(float dampingMultiplier)
    {
        Vector3 originalRotation = TPTarget.rotation.eulerAngles;
        xRotation = Mathf.Clamp(Mathf.DeltaAngle(0, xRotation), -VerticalRangeDown, VerticalRangeUp);
        float lerpFactor = (Damping > 0) ? 10 / (Damping * dampingMultiplier) * Time.unscaledDeltaTime : 1;
        TPTarget.rotation = Quaternion.Euler(
            originalRotation.x.AngleLerp(xRotation, lerpFactor),
            originalRotation.y.AngleLerp(yRotation, lerpFactor),
            0);
    }

    public void AlignCameraRotation(Transform transform)
    {
        Vector3 angle = transform.eulerAngles;
        xRotation = angle.x;
        yRotation = angle.y;
    }

    private void LockMouse(bool val)
    {
        Cursor.lockState = val ? CursorLockMode.Locked : CursorLockMode.None;
    }


    //Some helper stuff
    public Vector3 RotateAroundCameraY(Vector3 vector)
    {
        return Quaternion.AngleAxis(yRotation,Vector3.up) * vector;
    }

}