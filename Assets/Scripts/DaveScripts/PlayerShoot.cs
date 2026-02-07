using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
  [SerializeField] private LyraProjectile projectilePrefab;
  [SerializeField] private GameObject shotStartPos;
  [SerializeField] private GameObject projectilesRoot;

  private InputAction shootAction;
  private float shootInterval = 0.5f;
  private float lastShootTime = 0f;

  void Awake()
  {
    shootAction = InputSystem.actions["Shoot"];
  }

  void Start()
  {
  }

  void Update()
  {
    if (shootAction.IsPressed())
    {
      if (Time.time - lastShootTime >= shootInterval)
      {
        Shoot();
        lastShootTime = Time.time;
      }
    }
  }

  private void Shoot()
  {
    LyraProjectile projectile = Instantiate(projectilePrefab, shotStartPos.transform.position, Quaternion.identity, projectilesRoot.transform);
    Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
    Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
    Vector2 direction = (mouseWorldPos - shotStartPos.transform.position).normalized;
    projectile.Initialise(direction);
  }
}
