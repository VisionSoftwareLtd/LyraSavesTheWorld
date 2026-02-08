using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Reset();
    }

    public int superSpawnIfMobCountBelow = 15;

    public void Reset()
    {
        superSpawnIfMobCountBelow = 15;
    }
}
