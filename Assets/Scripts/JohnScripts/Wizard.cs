using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Animator))]
public class Wizard : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stoppingDistance = 5f;
    [SerializeField] private float retreatDistance = 3f;

    [Header("Attack")]
    [SerializeField] private float startTimeBtwShots = 1.5f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    private float timeBtwShots;
    private Player player;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        timeBtwShots = 0f;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        HandleMovement(distance);
        HandleShooting(distance);
    }

    void HandleMovement(float distance)
    {
        if (distance > stoppingDistance)
        {
            // Move towards player
            moveDirection = (player.transform.position - transform.position).normalized;
            animator.SetBool("IsAttacking", false);
        }
        else if (distance < retreatDistance)
        {
            // Move away from player
            moveDirection = (transform.position - player.transform.position).normalized;
            animator.SetBool("IsAttacking", false);
        }
        else
        {
            // In optimal range - stop moving
            moveDirection = Vector2.zero;
            animator.SetBool("IsAttacking", true);
        }
    }

    void HandleShooting(float distance)
    {
        if (distance <= stoppingDistance && distance > retreatDistance)
        {
            if (timeBtwShots <= 0f)
            {
                Instantiate(projectile, firePoint.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }
}
