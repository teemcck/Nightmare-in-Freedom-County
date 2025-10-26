using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class FloridamanAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private Transform player;

    [Header("Patrol Points (ordered)")]
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private float maxPatrolDistance = 10f;

    [Header("Sprites")]
    [SerializeField] private List<Sprite> walkSprites;
    [SerializeField] private List<Sprite> hitSprites;
    [SerializeField] private float walkAnimSpeed = 0.15f;
    [SerializeField] private float hitAnimSpeed = 0.1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Coroutine walkAnimRoutine;
    private int currentPointIndex = 0;
    private int patrolDirection = 1;
    private bool isChasing = false;
    private bool isHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (patrolPoints.Count == 0)
        {
            Debug.LogError("[FloridamanAI] ERROR: No patrol points assigned!");
            enabled = false;
            return;
        }

        walkAnimRoutine = StartCoroutine(WalkAnimation());
    }

    private void FixedUpdate()
    {
        if (isHit) return;

        Vector2 targetPos = PlayerInRange() ? (Vector2)player.position : (Vector2)patrolPoints[currentPointIndex].position;
        isChasing = PlayerInRange();

        Vector2 currentPos = rb.position;
        Vector2 dir = (targetPos - currentPos).normalized;

        if (dir == Vector2.zero)
            return;

        Vector2 newPos = currentPos + dir * moveSpeed * Time.fixedDeltaTime;

        if (!isChasing && Vector2.Distance(currentPos, targetPos) > maxPatrolDistance)
        {
            rb.position = patrolPoints[currentPointIndex].position; // teleport to current point
            AdvancePatrolPoint();
        }
        else
        {
            rb.MovePosition(newPos);
        }

        spriteRenderer.flipX = dir.x > 0;

        if (!isChasing && Vector2.Distance(currentPos, targetPos) < 0.2f)
        {
            AdvancePatrolPoint();
        }
    }

    private void AdvancePatrolPoint()
    {
        currentPointIndex += patrolDirection;

        if (currentPointIndex >= patrolPoints.Count)
        {
            patrolDirection = -1;
            currentPointIndex = patrolPoints.Count - 2;
        }
        else if (currentPointIndex < 0)
        {
            patrolDirection = 1;
            currentPointIndex = 1;
        }
    }

    private bool PlayerInRange()
    {
        if (player == null) return false;
        return Vector2.Distance(rb.position, player.position) <= detectionRadius;
    }

    private IEnumerator WalkAnimation()
    {
        if (walkSprites == null || walkSprites.Count == 0)
            yield break;

        int index = 0;
        bool forward = true;

        while (true)
        {
            spriteRenderer.sprite = walkSprites[index];
            yield return new WaitForSeconds(walkAnimSpeed);

            if (forward)
            {
                index++;
                if (index >= walkSprites.Count - 1) forward = false;
            }
            else
            {
                index--;
                if (index <= 0) forward = true;
            }
        }
    }

    private IEnumerator HitEffect()
    {
        isHit = true;
        if (walkAnimRoutine != null)
            StopCoroutine(walkAnimRoutine);

        foreach (var s in hitSprites)
        {
            spriteRenderer.sprite = s;
            yield return new WaitForSeconds(hitAnimSpeed);
        }

        walkAnimRoutine = StartCoroutine(WalkAnimation());
        isHit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(HitEffect());
    }
}
