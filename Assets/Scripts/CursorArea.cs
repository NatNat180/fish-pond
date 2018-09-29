using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorArea : MonoBehaviour
{

    public Texture2D cursorTexture;
    
    public CursorMode cursMode = CursorMode.Auto;

    void OnMouseOver()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(10, 5), cursMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursMode);
    }
}
