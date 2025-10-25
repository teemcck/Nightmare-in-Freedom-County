using System.Collections.Generic;
using UnityEngine;

public enum Location {
    UpperFloor,
    MainFloor,
    ExitHall,
    Cellar,
    Kitchen,
    Bathroom,
    Bedroom,
    MasterBedroom
}

public class MapHandler : MonoBehaviour
{
    private static MapHandler instance;

    // Store origin point for each location.
    private Dictionary<Location, Vector2> locationCordinates = new()
    {
        { Location.UpperFloor, new Vector2(0f, 0f) },
        { Location.MainFloor, new Vector2(0f, 0f) },
        { Location.ExitHall, new Vector2(0f, 0f) },
        { Location.Cellar, new Vector2(0f, 0f) },
        { Location.Kitchen, new Vector2(0f, 0f) },
        { Location.Bathroom, new Vector2(0f, 0f) },
        { Location.Bedroom, new Vector2(0f, 0f) },
        { Location.MasterBedroom, new Vector2(0f, 0f) }
    };

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
    
    public Vector2 GetLocation(Location location)
    {
        return locationCordinates[location];
    }
}