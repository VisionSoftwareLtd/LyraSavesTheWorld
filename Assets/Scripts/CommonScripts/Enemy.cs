using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D)), RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
  [SerializeField] private GameObject shadow;
  private SpriteRenderer spriteRenderer;
  private Rigidbody2D rb;
  private float initialShadowXPos;
  private Player collidingWithPlayer = null;
  private float maxOffScreenTime = 4f;
  private float offScreenDestroyTime;

  void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();
    if (shadow != null)
    {
      initialShadowXPos = shadow.transform.localPosition.x;
    }
  }

  void Start()
  {
    if (GetComponent<Cultist>() == null)
    {
      offScreenDestroyTime = Time.time + maxOffScreenTime;
    }
    else
    {
      offScreenDestroyTime = float.PositiveInfinity;
    }
  }

  private void Update()
  {
    if (IsOffScreen())
    {
      if (Time.time >= offScreenDestroyTime)
      {
        Destroy(gameObject);
      }
    }
    else
    {
      offScreenDestroyTime = Time.time + maxOffScreenTime;
    }
    if (rb.linearVelocityX < 0)
    {
      spriteRenderer.flipX = true;
      if (shadow != null)
      {
        shadow.transform.localPosition = new Vector3(-initialShadowXPos, shadow.transform.localPosition.y, shadow.transform.localPosition.z);
      }
    }
    else if (rb.linearVelocityX > 0)
    {
      spriteRenderer.flipX = false;
      if (shadow != null)
      {
        shadow.transform.localPosition = new Vector3(initialShadowXPos, shadow.transform.localPosition.y, shadow.transform.localPosition.z);
      }
    }
    if (collidingWithPlayer)
    {
      if (collidingWithPlayer != null) collidingWithPlayer.TakeDamage(10);
    }
  }

  private bool IsOffScreen()
  {
    Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
    return screenPoint.x < -0.1f || screenPoint.x > 1.1f || screenPoint.y < -0.1f || screenPoint.y > 1.1f;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.TryGetComponent(out Player player))
    {
      collidingWithPlayer = player;
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.TryGetComponent(out Player player))
    {
      collidingWithPlayer = null;
    }
  }

  public virtual void Hit(LyraProjectile lyraProjectile)
  {
    Damageable damageable = GetComponent<Damageable>();
    if (damageable != null && !damageable.CanDamage(lyraProjectile))
    {
      SoundManager.instance.PlaySoundRandomPitch("MonsterHurt");
      damageable.NonDamagingHit();
      return;
    }
    SoundManager.instance.PlaySoundRandomPitch("MonsterDie");
    Destroy(gameObject);
    if (!lyraProjectile.IsUpgraded) Destroy(lyraProjectile.gameObject);
  }
}
