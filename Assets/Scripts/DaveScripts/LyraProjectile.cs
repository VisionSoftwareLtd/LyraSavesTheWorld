using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LyraProjectile : MonoBehaviour
{
  [SerializeField] private bool isUpgraded = false;
  private Animator animator;

  void Awake()
  {
    animator = GetComponent<Animator>();
    animator.SetBool("IsUpgraded", isUpgraded);
  }

  void Start()
  {
  }

  void Update()
  {
  }
}
