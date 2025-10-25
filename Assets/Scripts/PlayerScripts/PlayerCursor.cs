using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private GameObject playerCursor;
    [SerializeField] private Sprite defaultCursor;
    [SerializeField] private Sprite interactableCursor;
    private Vector3 mouseScreenPosition;
    private SpriteRenderer cursorRenderer;
    private bool updateInteractable, updateDefault = false;

    void Start()
    {
        cursorRenderer = playerCursor.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mouseScreenPosition = Input.mousePosition;
        playerCursor.transform.position = mouseScreenPosition;

        if (updateInteractable && updateDefault)
        {
            Debug.LogError("Cursor is both interactable and default");
        }
        else if (updateInteractable)
        {
            cursorRenderer.sprite = interactableCursor;
            updateInteractable = false;
        }
        else if (updateDefault)
        {
            cursorRenderer.sprite = defaultCursor;
            updateDefault = false;
        }
    }

    public void SetInteractable()
    {
        updateInteractable = true;
    }

    public void SetDefault()
    {
        updateDefault = true;
    }
}