using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D)), RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  private Rigidbody2D rb;
  void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    if (rb.linearVelocityX < 0)
    {
      spriteRenderer.flipX = true;
    }
    else if (rb.linearVelocityX > 0)
    {
      spriteRenderer.flipX = false;
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.TryGetComponent(out Player player))
    {
      // player.TakeDamage(10f);
    }
  }

  public virtual void Hit(LyraProjectile lyraProjectile)
  {
    SoundManager.instance.PlaySoundRandomPitch("MonsterDie");
    Destroy(gameObject);
    Destroy(lyraProjectile.gameObject);
  }
}
