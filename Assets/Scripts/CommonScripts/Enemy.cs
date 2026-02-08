using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(10f);
        }
    }

    public virtual void Hit(LyraProjectile lyraProjectile)
    {

    }
}
