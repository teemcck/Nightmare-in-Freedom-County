using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private Vector3 playerPosition;

    void Update()
    {
        playerPosition = transform.position;
    }

    public Vector3 GetPosition()
    {
        return playerPosition;
    }
}
