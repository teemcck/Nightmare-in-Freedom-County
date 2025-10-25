using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private Sprite defaultCursor;
    [SerializeField] private Sprite interactableCursor;

    [SerializeField] private PlayerPosition playerPosition;
    [SerializeField] private float interactableRange = 10f;

    private SpriteRenderer cursorRenderer;
    private Camera mainCam;

    void Start()
    {
        cursorRenderer = GetComponent<SpriteRenderer>();
        cursorRenderer.sprite = defaultCursor;

        mainCam = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mouseWorldPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;

        CheckForInteractable(mouseWorldPosition);
    }

    private void CheckForInteractable(Vector3 worldPos)
    {
        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        if (hit != null && hit.TryGetComponent(out FloatingItem item))
        {
            Debug.Log("item hit");
            float distance = Vector2.Distance(playerPosition.GetPosition(), item.transform.position);

            if (distance <= interactableRange)
            {
                cursorRenderer.sprite = interactableCursor;
                return;
            }
        }

        cursorRenderer.sprite = defaultCursor;
    }
}
