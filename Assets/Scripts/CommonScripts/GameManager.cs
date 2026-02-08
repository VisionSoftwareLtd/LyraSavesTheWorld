using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  void Awake()
  {
    instance = this;
  }

  public int GameStage { get; private set; } = 0; // Increases by 1 each time a ritual is disrupted.
  public UI UI;

  IEnumerator Start()
  {
    yield return null;
    SoundManager.instance.PlayMusic($"Game{GameStage + 1}");
    UI.SetRitualText(GameStage);
  }

  public void RitualDisrupted()
  {
    GameStage++;
    if (GameStage == 4)
    {
      SceneManager.LoadScene("Win");
    }
    else
    {
      SoundManager.instance.PlayMusic($"Game{GameStage + 1}");
      UI.SetRitualText(GameStage);
    }
  }
}
