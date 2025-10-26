using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum DeathType
{
    Stabbing,
    Sanity
}

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private int DeathSceneIndex = 0;
    
    public void TriggerPlayerDeath(DeathType type)
    {
        
    }
}
