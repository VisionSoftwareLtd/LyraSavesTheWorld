using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerWinDetector : MonoBehaviour
{
  private int numRitualsDisrupted = 0;

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.TryGetComponent(out RitualZone ritualZone))
    {
      ritualZone.gameObject.SetActive(false);
      numRitualsDisrupted++;
      if (numRitualsDisrupted >= 4)
      {
        SceneManager.LoadScene("Win");
      }
    }
  }
}
