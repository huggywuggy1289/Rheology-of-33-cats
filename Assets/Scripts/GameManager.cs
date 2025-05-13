using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStageNum; // ���� ��������
    public int stageCatCount;   // ���� ������������ ã�� ����� ��
    public int totalCatCount;   // ��ü ������������ ã�� ����� ��
    public int[] catNumToFind   // �� ������������ ã�ƾ� �� ����� ��
        = { 1, 2, 5, 5, 10, 10 };
    public float totalTime;     // ������ �÷����� �ð�

    public bool isUIClosed;     // UI �г��� ��� ���� �÷��� ȭ������ üũ

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

    // ���� �ʱ�ȭ
    private void Init()
    {
        currentStageNum = SceneManager.GetActiveScene().buildIndex - 1;
        stageCatCount = 0;
        totalCatCount = 0;
        totalTime = 0;
        isUIClosed = true;
    }

    // �� ��ȯ �� stageNum�� stageCatCount �� �ʱ�ȭ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isUIClosed = true;
        currentStageNum = SceneManager.GetActiveScene().buildIndex - 1;
        stageCatCount = 0;

        SetResolution();
    }

    // ����̸� ã�� �� ã�� ����� ���� ǥ���ϴ� text UI ������Ʈ
    public void UpdateFoundCatInfo()
    {
        stageCatCount++;
        totalCatCount++;
    }

    // �ػ� �����ϴ� �Լ�
    public void SetResolution()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            // letterbox: ���Ͽ� ���� ��
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            // pillarbox: �¿쿡 ���� ��
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
        // ����̸� �� ã�� ���ߴٸ�
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
