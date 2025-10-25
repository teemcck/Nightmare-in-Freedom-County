using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    [SerializeField] private bool isUnlocked = true;
    [SerializeField] private Doorway connection;
    private Vector3 connectionPosition;
    private bool notInDoorway = true;

    // Singleton pattern.
    private void Awake()
    {
        connectionPosition = connection.gameObject.transform.position;
    }

    public void Unlock()
    {
        isUnlocked = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var tpState = collision.GetComponent<PlayerTeleportState>();
            if (tpState != null && !tpState.justTeleported && CanTravel())
            {
                tpState.justTeleported = true;
                collision.transform.position = connectionPosition;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var tpState = collision.GetComponent<PlayerTeleportState>();
            if (tpState != null)
                tpState.justTeleported = false;
        }
    }

    private bool CanTravel()
    {
        return isUnlocked && notInDoorway;
    }
}