using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private static SoundHandler instance;

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