using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour
{

    public Texture2D cursorTexture;
    public CursorMode cursMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseOver()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursMode);
    }
}
