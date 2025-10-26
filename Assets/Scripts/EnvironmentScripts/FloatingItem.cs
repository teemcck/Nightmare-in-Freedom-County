using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] private float interactableRange = 1.75f;
    [SerializeField] private float floatHeight = 0.25f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private GameObject itemDisplay;
    [SerializeField] private Item itemInfo;
    private bool doDestroy = false;

    private Vector3 startPosition;
    private PlayerPosition playerPosition;
    private PlayerInventory playerInventory;

    void Awake()
    {
        playerPosition = FindFirstObjectByType<PlayerPosition>();
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    private void Start()
    {
        startPosition = transform.position;
        itemDisplay.GetComponent<SpriteRenderer>().sprite = itemInfo.ItemSprite;
    }

    private void Update()
    {
        if (doDestroy)
        {
            Destroy(gameObject);
        }
        
        float newY = Mathf.Lerp(
            startPosition.y - floatHeight,
            startPosition.y + floatHeight,
            (Mathf.Sin(Time.time * floatSpeed) + 1f) / 2f
        );

        itemDisplay.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnMouseDown()
    {
        TryPickUpItem();
    }

    private void TryPickUpItem()
    {
        if (IsInteractable())
        {
            if (playerInventory.TryGetHeldItem(out Item heldItem))
            {
                Instantiate(heldItem.ItemPrefab, transform.position, Quaternion.identity);
            }
            playerInventory.UpdateHeldItem(itemInfo);

            doDestroy = true;
        }
    }

    public bool IsInteractable()
    {
        float distance = Vector2.Distance(playerPosition.GetPosition(), transform.position);

        if (distance <= interactableRange)
        {
            return true;
        }
        return false;
    }
}
