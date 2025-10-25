using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] PlayerInventoryUI invUI;
    private HeldItem heldItem;

    public class HeldItem
    {
        private string name;
        private Sprite itemSprite;

        public HeldItem(Item item)
        {
            name = item.ItemName;
            itemSprite = item.ItemSprite;
        }

        public string GetItemName()
        {
            return name;
        }

        public Sprite GetItemSprite()
        {
            return itemSprite;
        }
    }

    public void UpdateHeldItem(Item newItem)
    {
        heldItem = new(newItem);
        invUI.UpdateItemDisplay(heldItem.GetItemSprite());
    }
}