using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Cultist : MonoBehaviour, Damageable
{
  [SerializeField] private int facing = 0;
  private Animator animator;
  private AudioSource audioSource;
  private Material enemyMaterial;
  private float shieldFlashRatio = 0f;

  void Awake()
  {
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    enemyMaterial = new Material(spriteRenderer.material);
    spriteRenderer.material = enemyMaterial;
  }

  void Start()
  {
    animator.SetInteger("Facing", facing);
  }

  public void Initialise(float pitch)
  {
    audioSource.pitch = pitch;
  }

  private void Update()
  {
    if (shieldFlashRatio > 0f)
    {
      enemyMaterial.SetFloat("_ShieldRatio", shieldFlashRatio);
      shieldFlashRatio -= Time.deltaTime * 2f;
    }
    else
    {
      enemyMaterial.SetFloat("_ShieldRatio", 0f);
    }
  }

  public bool CanDamage(LyraProjectile lyraProjectile)
  {
    return lyraProjectile.IsUpgraded;
  }

  public void NonDamagingHit()
  {
    SoundManager.instance.PlaySoundRandomPitch("MonsterProtected");
    shieldFlashRatio = 1f;
  }
}