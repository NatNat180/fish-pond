using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public Texture2D cursorTexture;

    public CursorMode cursMode = CursorMode.Auto;

    void OnMouseOver()
    {
        if (Game.GameIsPaused == false)
        {
            Cursor.SetCursor(cursorTexture, new Vector2(10, 5), cursMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursMode);
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursMode);
    }
}
