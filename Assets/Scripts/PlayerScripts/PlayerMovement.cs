using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Range(0.0f, 10.0f), SerializeField] private float defaultPlayerSpeed = 2f;
    [Range(0.0f, 1.0f), SerializeField] private float slownessSpeedDecrease = 0.4f;
    [SerializeField] private List<Sprite> playerSprites;
    [SerializeField] private float animationSpeed = 0.15f; // time between frames


    // NEW!! Footsteps
    [SerializeField] private AudioSource walkingAudio;

    private float currentPlayerSpeed;
    private bool playerSlowed = false;
    private bool isMoving = false;

    private Rigidbody2D rb;
    [SerializeField] private GameObject spriteRenderer;
    private SpriteRenderer rendererComponent;
    private Vector2 movementInput;
    private Coroutine animCoroutine;
    private bool isWalking = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPlayerSpeed = defaultPlayerSpeed;
        rendererComponent = spriteRenderer.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movementInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) movementInput.y += 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) movementInput.y -= 1;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) movementInput.x -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) movementInput.x += 1;

        movementInput = movementInput.normalized;

        HandleWalkingAudio();
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            Vector2 targetPosition = rb.position + currentPlayerSpeed * Time.fixedDeltaTime * movementInput;
            rb.MovePosition(targetPosition);
        }
    }

    private IEnumerator AnimateSprites()
    {
        if (playerSprites == null || playerSprites.Count == 0)
            yield break;

        int index = 0;
        bool ascending = true;

        while (isMoving)
        {
            rendererComponent.sprite = playerSprites[index];

            yield return new WaitForSeconds(animationSpeed);

            if (ascending)
            {
                index++;
                if (index >= playerSprites.Count - 1)
                    ascending = false;
            }
            else
            {
                index--;
                if (index <= 0)
                    ascending = true;
            }
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

    private void HandleWalkingAudio()
    {
        // Player is moving
        if (movementInput != Vector2.zero)
        {
            if (!isWalking)
            {
                isWalking = true;

                if (walkingAudio != null && !walkingAudio.isPlaying)
                {
                    walkingAudio.loop = true;
                    walkingAudio.Play();
                }
            }
        }

    // Player stops moving
        else
        {
            if (isWalking)
            {
                isWalking = false;

                if (walkingAudio != null && walkingAudio.isPlaying)
                {
                    walkingAudio.Stop();
                }
            }
        }
    }
}
