using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InputInfoAsset")]
public class InputInfo : ScriptableObject
{
    public Vector2 mouseDelta;
    public Vector2 movementInput;
}
