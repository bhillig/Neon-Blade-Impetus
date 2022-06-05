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

    private CursorStates currentState;

    [System.Serializable]
    public struct CursorStateData
    {
        public CursorStates state;
        public List<Cursor> cursorImages;
    }

    [System.Serializable]
    public struct Cursor
    {
        public Image image;
        public Color c;
    }

    public List<CursorStateData> stateData;

    private void Awake()
    {
        instance = this;
    }

    public void SetCursorState(CursorStates state)
    {
        var prevState = stateData.Where(x => x.state == currentState).FirstOrDefault();
        foreach (var cursor in prevState.cursorImages)
        {
            cursor.image.enabled = false;
        }
        var foundState = stateData.Where(x => x.state == state).FirstOrDefault();
        foreach(var cursor in foundState.cursorImages)
        {
            cursor.image.enabled = true;
            cursor.image.color = cursor.c;
        }
        currentState = state;
    }
}
