using UnityEngine;

public class Enemy : MonoBehaviour, IMortal
{
    [SerializeField] private int points = 100;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float raycastDistance = 0.3f;
    [SerializeField] private LayerMask ignoreRaycast;

    private Rigidbody2D rb;
    private Animator anim;
    private UIManager uiManager;

    private Vector2[] movementDirections = { new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1) };
    private Vector2 currentDirection;

    private float changeRandomDirectionTime = 5f;
    private float changeRandomDirectionTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();

        currentDirection = GetRandomDirection();
        changeRandomDirectionTimer = changeRandomDirectionTime;
    }

    private void Update()
    {
        if (changeRandomDirectionTimer < 0)
        {
            changeRandomDirectionTimer = changeRandomDirectionTime;
            currentDirection = GetRandomDirection();
        }
        else
        {
            changeRandomDirectionTimer -= Time.deltaTime;
        }

        if (IsPathBlocked())
        {
            currentDirection = GetRandomDirection();
        }

        SetAnimationParameters(currentDirection);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void SetAnimationParameters(Vector2 direction)
    {
        anim.SetFloat("Horizontal", direction.x);
        anim.SetFloat("Vertical", direction.y);
    }

    private bool IsPathBlocked()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, raycastDistance, ignoreRaycast);

        Debug.DrawRay(transform.position, currentDirection * raycastDistance, Color.red);
        if (hit)
        {
            return true;
        }

        return false;
    }

    private Vector2 GetRandomDirection()
    {
        return movementDirections[Random.Range(0, movementDirections.Length)];
    }

    public void IncreaseMoveSpeed(float addSpeed)
    {
        moveSpeed += addSpeed;
        Debug.Log("MOveSpeed " + moveSpeed);
    }

    public void Die()
    {

        anim.SetTrigger("IsDead");

        this.GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;

        uiManager.UpdateScore(points);
        Destroy(gameObject, 1f);
    }
}
