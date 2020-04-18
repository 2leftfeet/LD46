using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    private enum CursorStates
    {
        DEFAULT,
        CAST
    }
    private CursorStates state;
    [SerializeField] private Sprite defaultCursor = default;
    [SerializeField] private Sprite castCursor = default;

    private RectTransform cursorRect;
    private Image cursorImage;

    private Vector2 defaultPivot;
    private Vector2 castPivot;

    private void Awake()
    {
        instance = this;
        cursorRect = transform.GetComponent<RectTransform>();
        cursorImage = transform.GetComponent<Image>();
        state = CursorStates.DEFAULT;

        defaultPivot = new Vector2(0.12f, 0.88f);
        castPivot = new Vector2(0.5f, 0.5f);
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // No idea why I have to always hide it but it doesn't work only on start.
        Cursor.visible = false;
        Vector3 mousePos = Input.mousePosition;
        cursorRect.position = mousePos;
    }

    public void SetCursorState(string which)
    {
        if(which == "CAST")
        {
            state = CursorStates.CAST;
            cursorImage.sprite = castCursor;
            cursorRect.pivot = castPivot;
        } 
        else if(which == "DEFAULT")
        {
            state = CursorStates.DEFAULT;
            cursorImage.sprite = defaultCursor;
            cursorRect.pivot = defaultPivot;
        }
    }
}
