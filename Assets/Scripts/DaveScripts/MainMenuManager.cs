using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null;
        SoundManager.instance.PlayMusic("MenuIntro");
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Intro");
    }
}
