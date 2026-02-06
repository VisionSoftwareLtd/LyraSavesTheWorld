using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 10f;
  [SerializeField] private float acceleration = 20f;
  [SerializeField] private float deceleration = 50f;
  private Rigidbody2D rb;
  private Animator animator;
  private InputAction moveAction;
  private CinemachinePositionComposer positionComposer;
  private Vector3 lookAheadOffset = new Vector3(0, 0, 0);

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    moveAction = InputSystem.actions["Move"];
    positionComposer = FindFirstObjectByType<CinemachinePositionComposer>();
  }

  void Start()
  {
  }

  void FixedUpdate()
  {
    Vector2 input = moveAction.ReadValue<Vector2>();
    if (input != Vector2.zero)
    {
      animator.SetBool("IsMoving", true);
      animator.SetFloat("XSpeed", input.x);
      animator.SetFloat("YSpeed", input.y);
    }
    else
    {
      animator.SetBool("IsMoving", false);
    }
    Vector2 targetVelocity = input.normalized * moveSpeed;
    Vector2 velocityDifference = targetVelocity - rb.linearVelocity;
    Vector2 force = velocityDifference * (velocityDifference.magnitude > 0 ? acceleration : deceleration);
    rb.AddForce(force);
    lookAheadOffset = Vector3.Lerp(lookAheadOffset, new Vector3(input.x, input.y, 0), Time.fixedDeltaTime * 3f);
    positionComposer.TargetOffset = Vector3.Lerp(positionComposer.TargetOffset, lookAheadOffset, Time.fixedDeltaTime * 3f);
  }

  void Update()
  {
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.TryGetComponent(out WinZone winZone))
    {
      SceneManager.LoadScene("Win");
    }
  }
}
