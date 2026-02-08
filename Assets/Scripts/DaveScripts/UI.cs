using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
  [SerializeField] private Slider healthBar;
  [SerializeField] private TextMeshProUGUI ritualText;

  public void SetHealthBar(float healthPercent)
  {
    healthBar.value = healthPercent;
  }

  public void SetRitualText(int gameStage)
  {
    ritualText.text = $"Rituals Disrupted: <sketchy>{gameStage}</sketchy>/<sketchy>4</sketchy>";
  }
}
