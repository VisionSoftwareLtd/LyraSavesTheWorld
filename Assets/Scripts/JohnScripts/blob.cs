using UnityEngine;

public class Blob : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 2f;
  Rigidbody2D rb;
  Player target;
  Vector2 moveDirection;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    target = FindFirstObjectByType<Player>();
  }

  // Update is called once per frame
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
    if (target)
    {
      rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
    }
  }
}
