using UnityEngine;

public class HiddenItem : MonoBehaviour
{
    [SerializeField] private float interactableRange = 1.75f;
    [SerializeField] private float searchTime = 2f; // Seconds
    [SerializeField] private PlayerPosition playerPosition;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject unsearchedObject;
    [SerializeField] private GameObject searchedObject;
    private bool hasBeenSearched = false;
    private float currentSearchTime;
    private Item itemInfo;

    void Start()
    {
        unsearchedObject.SetActive(true);
    }

    public void SetHiddenItem(Item item)
    {
        itemInfo = item;
    }

    private void OnMouseDown()
    {
        if (IsInteractable())
        {
            ContinueSearch();
        }
    }

    private void OnMouseUp()
    {
        currentSearchTime = 0f;
    }

    private void ContinueSearch()
    {
        currentSearchTime += Time.deltaTime;

        if (currentSearchTime >= searchTime)
        {
            Instantiate(itemInfo.ItemPrefab, transform.position, Quaternion.identity);
            unsearchedObject.SetActive(false); searchedObject.SetActive(true);
            hasBeenSearched = true;
        }
    }

    public bool IsInteractable()
    {
        if (hasBeenSearched) return false;

        float distance = Vector2.Distance(playerPosition.GetPosition(), transform.position);

        if (distance <= interactableRange)
        {
            return true;
        }
        return false;
    }
}
