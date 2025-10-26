using Unity.VisualScripting;
using UnityEngine;

public class HiddenItem : MonoBehaviour
{
    [SerializeField] private float interactableRange = 3f;
    private float searchTime = 2f; // Do not edit.
    [SerializeField] private PlayerPosition playerPosition;
    [SerializeField] private GameObject unsearchedObject;
    [SerializeField] private GameObject searchedObject;
    [SerializeField] private PlayerSearch playerSearch;
    [SerializeField] private Vector3 itemOffset;
    private bool hasBeenSearched = false;
    private float currentSearchTime;
    [SerializeField] private Item itemInfo;
    private Camera mainCam;

    void Start()
    {
        unsearchedObject.SetActive(true);
        searchedObject.SetActive(false);
        mainCam = Camera.main;
    }

    public void SetHiddenItem(Item item)
    {
        itemInfo = item;
    }

    void Update()
    {
        if (hasBeenSearched) return;

        // Not great design but running out of time.
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.transform.IsChildOf(transform))
        {
            if (IsInteractable() && Input.GetMouseButton(0))
            {
                ContinueSearch();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                currentSearchTime = 0f;
                playerSearch.UpdateSearch(currentSearchTime);
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                currentSearchTime = 0f;
                playerSearch.UpdateSearch(currentSearchTime);
            }
        }
    }

    private void ContinueSearch()
    {
        currentSearchTime += Time.deltaTime;
        playerSearch.UpdateSearch(currentSearchTime);

        if (currentSearchTime >= searchTime)
        {
            Instantiate(itemInfo.ItemPrefab, unsearchedObject.transform.position + itemOffset, Quaternion.identity);
            unsearchedObject.SetActive(false);
            searchedObject.SetActive(true);
            hasBeenSearched = true;
        }
    }

    public bool IsInteractable()
    {
        if (hasBeenSearched) return false;
        float distance = Vector2.Distance(playerPosition.GetPosition(), unsearchedObject.transform.position);
        return distance <= interactableRange;
    }
}
