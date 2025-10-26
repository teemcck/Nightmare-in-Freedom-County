using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public LayerMask interactableLayer; // Set in inspector to only include "Interactable"

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast that only hits the Interactable layer
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, interactableLayer);

            if (hit.collider != null)
            {
                ClickableObject clickable = hit.collider.GetComponent<ClickableObject>();
                if (clickable != null)
                {
                    clickable.OnObjectClicked(); // Custom method we'll make below
                }
            }
        }
    }
}
