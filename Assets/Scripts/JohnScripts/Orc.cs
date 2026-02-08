using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector2 retargetIntervalRange = new Vector2(2f, 5f);
    private float nextRetargetTime;
    Rigidbody2D rb;
    Player target;
    Vector2 moveDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetTarget();
    }

    private void SetTarget()
    {
        target = FindFirstObjectByType<Player>();

        if (target)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            moveDirection = direction;
        }
        nextRetargetTime = Time.time + Random.Range(retargetIntervalRange.x, retargetIntervalRange.y);
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    private void Update()
    {
        if (Time.time >= nextRetargetTime)
        {
            SetTarget();
        }
    }
}
