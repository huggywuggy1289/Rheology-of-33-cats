using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int stageCatCount = 0;
    public int[] catNumToFind = { 1, 2, 5, 5, 10, 10 };
    public int currentStageNum = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
