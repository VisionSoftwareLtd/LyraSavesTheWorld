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

    public void OnClickStartEasy()
    {
        PersistentData.instance.superSpawnIfMobCountBelow = 8;
        SceneManager.LoadScene("Game");
    }

    public void OnClickStartMedium()
    {
        PersistentData.instance.superSpawnIfMobCountBelow = 15;
        SceneManager.LoadScene("Game");
    }

    public void OnClickStartHard()
    {
        PersistentData.instance.superSpawnIfMobCountBelow = 20;
        SceneManager.LoadScene("Game");
    }
}
