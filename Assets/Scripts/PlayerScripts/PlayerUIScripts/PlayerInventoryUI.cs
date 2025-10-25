using UnityEngine.UI;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private Image itemDisplay;
    [SerializeField] private Image itemSlot;

    public void UpdateItemDisplay(Sprite newItem)
    {
        itemDisplay.sprite = newItem;
    }

    // Add an effect to item slot when item is picked up later.
}