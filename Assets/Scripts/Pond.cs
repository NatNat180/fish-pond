using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour
{

    public Transform player;
    public Texture2D cursorTexture;
    public CursorMode cursMode = CursorMode.Auto;
	public static bool PlayerCanCast = false;
    private float distFromPlayerToCursor;

    void Update()
    {
        // get cursor and player position
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(player.position);
        distFromPlayerToCursor = Vector3.Distance(mousePosition, playerPosition);
    }

    void OnMouseOver()
    {
        if (distFromPlayerToCursor < 150f)
        {
			PlayerCanCast = true;
            Cursor.SetCursor(cursorTexture, new Vector2(10, 5), cursMode);
        }
        else
        {
			PlayerCanCast = false;
            Cursor.SetCursor(null, Vector2.zero, cursMode);
        }
    }

    void OnMouseExit()
    {
		PlayerCanCast = false;
        Cursor.SetCursor(null, Vector2.zero, cursMode);
    }
}
