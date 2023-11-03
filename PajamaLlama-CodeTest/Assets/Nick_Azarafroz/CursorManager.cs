using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTextture;

    private Vector3 cursorHotspot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCursor() 
    {
        cursorHotspot = new Vector2(cursorTextture.width / 2f, cursorTextture.height / 2f);
        Cursor.SetCursor(cursorTextture, cursorHotspot, CursorMode.Auto);
    }
}
