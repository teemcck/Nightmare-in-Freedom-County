using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.0f, 10.0f), SerializeField] private float defaultPlayerSpeed = 2f;

    private float currentPlayerSpeed;
    // For when the environmental slow effects xd.
    [Range(0.0f, 1.0f), SerializeField] private float slownessSpeedDecrease = 0.4f;
    private bool playerSlowed = false;

    void Awake()
    {
        currentPlayerSpeed = defaultPlayerSpeed;
    }

    void Update()
    {
        ProcessKeyInput();
    }

    private void ProcessKeyInput()
    {
        Vector3 newPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
           newPosition.y += currentPlayerSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            newPosition.x -= currentPlayerSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            newPosition.y -= currentPlayerSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            newPosition.x += currentPlayerSpeed * Time.deltaTime;
        }
        transform.position = newPosition;
    }
    
    public void SetSlowness(bool flag)
    {
        if (flag && !playerSlowed)
        {
            playerSlowed = true;
            currentPlayerSpeed = defaultPlayerSpeed * (1 - slownessSpeedDecrease);
        }
        else
        {
            playerSlowed = false;
            currentPlayerSpeed = defaultPlayerSpeed;
        }
    }
}