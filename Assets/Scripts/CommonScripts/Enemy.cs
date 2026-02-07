using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
  void Start()
  {
  }

  void Update()
  {
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.TryGetComponent(out Player player))
    {
      player.TakeDamage(10f);
      SoundManager.instance.PlaySoundRandomPitch($"Hurt{Random.Range(1, 4)}");
    }
  }

  internal void Hit(LyraProjectile lyraProjectile)
  {
    SoundManager.instance.PlaySoundRandomPitch("MonsterDie");
    Destroy(gameObject);
  }
}
