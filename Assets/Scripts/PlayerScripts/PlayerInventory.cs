using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] PlayerInventoryUI invUI;
    private static PlayerInventory instance;
    private Item heldItem;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void UpdateHeldItem(Item newItem)
    {
        heldItem = newItem;
        invUI.UpdateItemDisplay(heldItem.ItemSprite);
    }

    public void ResetHeldItem()
    {
        heldItem = null;
        invUI.ResetItemDisplay();
    }

    public bool TryGetHeldItem(out Item item)
    {
        if (heldItem != null)
        {
            item = heldItem;
            return true;
        }
        item = null;
        return false;
    }
}