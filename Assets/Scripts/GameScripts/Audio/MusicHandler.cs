using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private static MusicHandler instance;

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