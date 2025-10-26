using Unity.VisualScripting;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private Vector3 playerPosition;
    private static PlayerPosition instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Update()
    {
        playerPosition = transform.position;
    }

    public Vector3 GetPosition()
    {
        return playerPosition;
    }
}
