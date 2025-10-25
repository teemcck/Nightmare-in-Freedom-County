using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Range(0.0f, 10.0f), SerializeField] private float defaultPlayerSpeed = 2f;

    private float currentPlayerSpeed;
    // For when the environmental slow effects xd.
    [Range(0.0f, 1.0f), SerializeField] private float slownessSpeedDecrease = 0.4f;
    private bool playerSlowed = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPlayerSpeed = defaultPlayerSpeed;
    }

    void Update()
    {
        ProcessKeyInput();
    }

    private void ProcessKeyInput()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) input.y += 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) input.y -= 1;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) input.x -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) input.x += 1;

        if (input != Vector2.zero)
        {
            input.Normalize();
            Vector2 targetPosition = rb.position + currentPlayerSpeed * Time.fixedDeltaTime * input;
            rb.MovePosition(targetPosition);
        }
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