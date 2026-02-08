using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Animator))]
public class Knight : MonoBehaviour, Damageable
{
  public enum KnightState
  {
    Moving,
    Attacking
  }

  [SerializeField] private float moveSpeed = 0.5f;
  [SerializeField] private float attackDistance = 0.5f;
  [SerializeField] private float attackSpeed = 5f;
  [SerializeField] private float attackCooldown = 2f;
  private Rigidbody2D rb;
  private Player target;
  private Animator animator;
  private KnightState currentState;
  private Vector2 moveDirection;
  private float finishAttackTime;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  void Start()
  {
    target = FindFirstObjectByType<Player>();
    currentState = KnightState.Moving;
  }

  void Update()
  {
    if (target)
    {
      Vector3 direction = (target.transform.position - transform.position).normalized;
      moveDirection = direction;
    }
  }

  private void FixedUpdate()
  {
    if (currentState == KnightState.Moving)
    {
      if (target)
      {
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        animator.SetBool("IsAttacking", false);
        if (Vector2.Distance(transform.position, target.transform.position) <= attackDistance)
        {
          currentState = KnightState.Attacking;
          rb.linearVelocity = Vector2.zero;
          Vector2 direction = (target.transform.position - transform.position).normalized;
          rb.AddForce(direction * attackSpeed, ForceMode2D.Impulse);
          animator.SetBool("IsAttacking", true);
          finishAttackTime = Time.time + attackCooldown;
          SoundManager.instance.PlaySoundRandomPitch("KnightAttack");
        }
      }
    }
    else if (currentState == KnightState.Attacking)
    {
      if (Time.time >= finishAttackTime)
      {
        currentState = KnightState.Moving;
        animator.SetBool("IsAttacking", false);
      }
    }
  }

  public bool CanDamage(LyraProjectile lyraProjectile)
  {
    return lyraProjectile.IsUpgraded;
  }
}
