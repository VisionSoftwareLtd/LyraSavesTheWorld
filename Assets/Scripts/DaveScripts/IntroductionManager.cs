using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null;
        SoundManager.instance.PlayMusic("MenuIntro");
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Game");
    }
}
