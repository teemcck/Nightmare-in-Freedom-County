using UnityEngine.UI;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject itemDisplay;
    [SerializeField] private GameObject itemSlot;

    void Awake()
    {
        itemDisplay.SetActive(false);
    }

    public void UpdateItemDisplay(Sprite newItem)
    {
        // Make sure active.
        itemDisplay.SetActive(true);
        itemDisplay.GetComponent<Image>().sprite = newItem;
    }

    public void ResetItemDisplay()
    {
        itemDisplay.SetActive(false);
    }

    // Add an effect to item slot when item is picked up later.
}