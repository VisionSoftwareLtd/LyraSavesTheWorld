using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
  private float health = 100;
  [SerializeField] private float damageImmuneDuration = 0.5f;
  private float immuneUntilTime = 0f;

  void Start()
  {
    GameManager.instance.UI.SetHealthBar(health / 100f);
  }

  public void TakeDamage(float damage)
  {
    if (Time.time < immuneUntilTime) return;
    immuneUntilTime = Time.time + damageImmuneDuration;
    health -= damage;
    if (health <= 0)
    {
      SceneManager.LoadScene("Lose");
      return;
    }
    GameManager.instance.UI.SetHealthBar(health / 100f);
    SoundManager.instance.PlaySound($"Hurt{Random.Range(1, 4)}");
  }
}
