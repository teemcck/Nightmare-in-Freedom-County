using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] private float floatHeight = 0.25f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private Item itemInfo;
    private bool doDestroy = false;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        gameObject.GetComponent<SpriteRenderer>().sprite = itemInfo.ItemSprite;
    }

    private void Update()
    {
        if (doDestroy)
        {
            Destroy(this);
        }
        
        float newY = Mathf.Lerp(
            startPosition.y - floatHeight,
            startPosition.y + floatHeight,
            (Mathf.Sin(Time.time * floatSpeed) + 1f) / 2f
        );

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Interacted with by the player, should make this the players held item.
    public Item CollectItem()
    {
        doDestroy = true;
        return itemInfo;
    }
}
