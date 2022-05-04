using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "InputInfoAsset")]
public class InputInfo : ScriptableObject
{
    public Vector2 mouseDelta;
    public Vector2 movementInput;
    public UnityEvent SpacebarDownEvent;
    public UnityEvent SpacebarUpEvent;
}
