using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  private Rigidbody2D rb;
  private InputAction moveAction;
  [SerializeField] private float moveSpeed = 10f;

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    moveAction = InputSystem.actions["Move"];
  }

  void Start()
  {
  }

  void Update()
  {
    Vector2 input = moveAction.ReadValue<Vector2>();
    Vector2 movement = input.normalized * moveSpeed;
    rb.linearVelocity = movement;
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.TryGetComponent(out WinZone winZone))
    {
      SceneManager.LoadScene("Win");
    }
  }
}
