using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStageNum; // 현재 스테이지
    public int stageCatCount;   // 현재 스테이지에서 찾은 고양이 수
    public int totalCatCount;   // 전체 스테이지에서 찾은 고양이 수
    public int[] catNumToFind   // 각 스테이지에서 찾아야 할 고양이 수
        = { 1, 2, 5, 5, 10, 10 };
    public float totalTime;     // 게임을 플레이한 시간

    public bool isUIClosed;     // UI 패널이 모두 닫힌 플레이 화면인지 체크

    private float targetAspect = 16f / 9f;

    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

        Init();
        SetResolution();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 변수 초기화
    private void Init()
    {
        currentStageNum = SceneManager.GetActiveScene().buildIndex - 1;
        stageCatCount = 0;
        totalCatCount = 0;
        totalTime = 0;
        isUIClosed = true;
    }

    // 씬 전환 시 stageNum과 stageCatCount 값 초기화
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isUIClosed = true;
        currentStageNum = SceneManager.GetActiveScene().buildIndex - 1;
        stageCatCount = 0;

        SetResolution();
    }

    // 고양이를 찾을 시 찾은 고양이 수를 표기하는 text UI 업데이트
    public void UpdateFoundCatInfo()
    {
        stageCatCount++;
        totalCatCount++;
    }

    // 해상도 설정하는 함수
    public void SetResolution()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            // letterbox: 상하에 검은 띠
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            // pillarbox: 좌우에 검은 띠
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    public bool GameOver()
    {
        // 고양이를 다 찾지 못했다면
        if (stageCatCount < catNumToFind[currentStageNum - 1])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
