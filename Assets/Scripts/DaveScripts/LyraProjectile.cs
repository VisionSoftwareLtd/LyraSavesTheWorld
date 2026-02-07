using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody2D))]
public class LyraProjectile : MonoBehaviour
{
  [SerializeField] private bool isUpgraded = false;
  [SerializeField] private float speed = 10f;
  private Animator animator;
  private Rigidbody2D rb;
  private Vector2 initialDirection;

  void Awake()
  {
    animator = GetComponent<Animator>();
    animator.SetBool("IsUpgraded", isUpgraded);
    rb = GetComponent<Rigidbody2D>();
  }

  void Start()
  {
    rb.linearVelocity = initialDirection * speed;
  }

  internal void Initialise(Vector2 direction)
  {
    initialDirection = direction.normalized;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("CausesUpgrade"))
    {
      if (isUpgraded)
      {
        Destroy(gameObject);
      }
      else
      {
        isUpgraded = true;
        animator.SetBool("IsUpgraded", true);
        SoundManager.instance.PlaySoundRandomPitch("ShotBounce");
      }
    }
    else if (collision.gameObject.TryGetComponent(out Enemy enemy))
    {
      enemy.Hit(this);
    }
  }
}
