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

  private int gameStage = 0; // Increases by 1 each time a ritual is disrupted.

  IEnumerator Start()
  {
    yield return null;
    SoundManager.instance.PlayMusic($"Game{gameStage + 1}");
  }

  public void RitualDisrupted()
  {
    gameStage++;
    if (gameStage == 4)
    {
      SceneManager.LoadScene("Win");
    }
    else
    {
      SoundManager.instance.PlayMusic($"Game{gameStage + 1}");
    }
  }
}
