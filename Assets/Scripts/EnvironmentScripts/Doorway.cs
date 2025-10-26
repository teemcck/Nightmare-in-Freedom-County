using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    [SerializeField] private bool isUnlocked = true;
    [SerializeField] private Doorway connection;
    private Vector3 tpPos;

    // Singleton pattern.
    private void Awake()
    {
        tpPos = transform.Find("TeleportPoint").position;
    }

    public void Unlock()
    {
        isUnlocked = true;
    }

    public Vector3 GetDestination()
    {
        return tpPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.CompareTag("Player") && isUnlocked)
        {
            collision.gameObject.transform.position = connection.GetDestination();  
        }
    }
}