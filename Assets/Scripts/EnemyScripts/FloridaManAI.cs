using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class FloridamanAI : MonoBehaviour
{
    [System.Serializable]
    public class Room
    {
        public string roomName;
        public List<Transform> waypoints = new List<Transform>();
    }

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private Transform player;

    [Header("Rooms & Waypoints (ordered)")]
    [SerializeField] private List<Room> rooms = new List<Room>();

    [Header("Sprites")]
    [SerializeField] private List<Sprite> walkSprites;
    [SerializeField] private List<Sprite> hitSprites;
    [SerializeField] private float walkAnimSpeed = 0.15f;
    [SerializeField] private float hitAnimSpeed = 0.1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Coroutine walkAnimRoutine;
    private int currentRoomIndex = 0;
    private int currentWaypointIndex = 0;
    private int direction = 1; // 1 = forward, -1 = backward
    private bool isHit = false;
    private bool isChasing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (rooms.Count == 0 || rooms[0].waypoints.Count == 0)
        {
            enabled = false;
            return;
        }

        transform.position = rooms[0].waypoints[0].position;

        walkAnimRoutine = StartCoroutine(WalkAnimation());
    }

    private void FixedUpdate()
    {
        if (isHit) return;

        Vector2 targetPos = GetCurrentTargetPosition();
        isChasing = PlayerInRange();

        Vector2 currentPos = rb.position;
        Vector2 dir = (targetPos - currentPos).normalized;

        if (dir == Vector2.zero) return;

        Vector2 newPos = currentPos + dir * moveSpeed * Time.fixedDeltaTime;

        if (!isChasing)
        {
            rb.MovePosition(newPos);

            if (Vector2.Distance(currentPos, targetPos) < 0.2f)
            {
                AdvanceWaypoint();
            }
        }
        else
        {
            // Chase player if in range
            rb.MovePosition(currentPos + dir * moveSpeed * Time.fixedDeltaTime);
        }

        spriteRenderer.flipX = dir.x > 0;
    }

    private Vector2 GetCurrentTargetPosition()
    {
        if (isChasing) return player.position;
        return rooms[currentRoomIndex].waypoints[currentWaypointIndex].position;
    }

    private void AdvanceWaypoint()
    {
        Room currentRoom = rooms[currentRoomIndex];

        currentWaypointIndex += direction;

        if (currentWaypointIndex >= currentRoom.waypoints.Count || currentWaypointIndex < 0)
        {
            currentRoomIndex += direction;

            if (currentRoomIndex >= rooms.Count)
            {
                direction = -1;
                currentRoomIndex = rooms.Count - 1;
            }
            else if (currentRoomIndex < 0)
            {
                direction = 1;
                currentRoomIndex = 0;
            }

            // Teleport to first waypoint of next room
            currentRoom = rooms[currentRoomIndex];
            currentWaypointIndex = direction == 1 ? 0 : currentRoom.waypoints.Count - 1;

            transform.position = currentRoom.waypoints[currentWaypointIndex].position;
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
