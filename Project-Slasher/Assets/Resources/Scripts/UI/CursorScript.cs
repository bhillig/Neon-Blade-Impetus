using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public enum CursorStates
{ 
    Default,
    Enemy
}


public class CursorScript : MonoBehaviour
{
    public static CursorScript instance;

    private Image cursorImage;

    [System.Serializable]
    public struct CursorStateData
    {
        public CursorStates state;
        public Color c;
    }

    public List<CursorStateData> stateData;

    private void Awake()
    {
        instance = this;
        cursorImage = GetComponent<Image>();
    }

    public void SetCursorState(CursorStates state)
    {
        var foundState = stateData.Where(x => x.state == state).FirstOrDefault();
        cursorImage.color = foundState.c;
    }
}
