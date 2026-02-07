using System;
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
    }
  }

  internal void Hit(LyraProjectile lyraProjectile)
  {
    Destroy(gameObject);
  }
}
