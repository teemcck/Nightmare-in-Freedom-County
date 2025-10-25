using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Range(0.0f, 10.0f), SerializeField] private float defaultPlayerSpeed = 2f;
    [Range(0.0f, 1.0f), SerializeField] private float slownessSpeedDecrease = 0.4f;

    private float currentPlayerSpeed;
    private bool playerSlowed = false;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPlayerSpeed = defaultPlayerSpeed;
    }

    void Update()
    {
        movementInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) movementInput.y += 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) movementInput.y -= 1;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) movementInput.x -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) movementInput.x += 1;

        movementInput = movementInput.normalized;
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            Vector2 targetPosition = rb.position + currentPlayerSpeed * Time.fixedDeltaTime * movementInput;
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
