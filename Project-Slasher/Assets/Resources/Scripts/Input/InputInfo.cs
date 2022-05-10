using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "InputInfoAsset")]
public class InputInfo : ScriptableObject
{
    public Vector2 mouseDelta;
    public Vector2 movementInput;
    public Vector2 lastNZeroMovementInput;
    public UnityEvent JumpDownEvent;
    public UnityEvent JumpUpEvent;
    public UnityEvent SlideDownEvent;
    public UnityEvent SlideUpEvent;
    public bool slideDown;

    public UnityEvent PrimaryDownEvent;
    public UnityEvent PrimaryUpEvent;
    public bool primaryDown;
}
