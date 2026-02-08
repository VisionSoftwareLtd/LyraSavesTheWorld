using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Cultist : MonoBehaviour, Damageable
{
  [SerializeField] private int facing = 0;
  private Animator animator;
  private AudioSource audioSource;

  void Awake()
  {
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
  }

  void Start()
  {
    animator.SetInteger("Facing", facing);
  }

  public void Initialise(float pitch)
  {
    audioSource.pitch = pitch;
  }

  public bool CanDamage(LyraProjectile lyraProjectile)
  {
    return lyraProjectile.IsUpgraded;
  }

}
