using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    private static MapHandler instance;

    // Singleton pattern.
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    } 
}