using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
  public void TakeDamage(float damage)
  {
    Debug.Log($"Player takes {damage} damage!");
    SceneManager.LoadScene("Lose");
  }
}
