using UnityEngine;

public class RitualZone : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  [SerializeField] private Sprite deadPentagram;
  private bool ritualActive = true;

  void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Start()
  {
    Cultist[] cultists = GetComponentsInChildren<Cultist>();
    for (int i = 0; i < cultists.Length; i++)
    {
      cultists[i].Initialise(0.9f + (i * 0.15f));
    }
  }

  private void Update()
  {
    if (!ritualActive) return;

    if (transform.childCount == 0)
    {
      spriteRenderer.sprite = deadPentagram;
      SoundManager.instance.PlaySoundRandomPitch("RitualExplode");
      GameManager.instance.RitualDisrupted();
      ritualActive = false;
    }
  }
}
