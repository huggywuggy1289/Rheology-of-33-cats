using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    public TMP_Text stageText;
    public TMP_Text timeText;
    public TMP_Text gameOverText;
    public TMP_Text clearText;
    public TMP_Text finalClearText;   // �� ���� Ŭ����� �޽���

    public GameObject popUpPanel;
    public GameObject restartBtn;
    public GameObject nextStageBtn;

    float time;
    int min, sec, stage;
    bool timerStopped = false;

    void Start()
    {
        stage = GameManager.Instance.currentStageNum;

        // ���������� �ð� ����
        if (stage <= 2)
            time = 20f;
        else if (stage <= 4)
            time = 30f;
        else
            time = 45f;

        min = (int)time / 60;
        sec = (int)time % 60;

        stageText.text = stage.ToString() + "�ܰ�";
        timeText.text = min.ToString("D2") + " : " + sec.ToString("D2");

        popUpPanel.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        clearText.gameObject.SetActive(false);
        finalClearText.gameObject.SetActive(false);
        restartBtn.SetActive(false);
        nextStageBtn.SetActive(false);
    }

    void Update()
    {
        if (timerStopped) return;

        time -= Time.deltaTime;
        GameManager.Instance.totalTime += Time.deltaTime;

        min = (int)time / 60;
        sec = (int)time % 60;

        if (time <= 0f)
        {
            time = 0f;
            timeText.text = "00 : 00";
            StartCoroutine(HandleGameOver());
            timerStopped = true;
        }
        else
        {
            timeText.text = min.ToString("D2") + " : " + sec.ToString("D2");

            // ����� �� ã�� ���
            int stage = GameManager.Instance.currentStageNum;
            if (GameManager.Instance.stageCatCount >= GameManager.Instance.catNumToFind[stage - 1])
            {
                timerStopped = true;

                if (stage == 6)
                    StartCoroutine(HandleFinalClear());
                else
                    StartCoroutine(HandleClear());
            }
        }
    }

    IEnumerator HandleGameOver()
    {
        GameManager.Instance.isUIClosed = false;

        gameOverText.text = stage.ToString() + "�ܰ� Ÿ�� �ƿ�!\n\n" +
            "ã�� ����� �� : " + GameManager.Instance.totalCatCount.ToString() + "����";

        popUpPanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        restartBtn.SetActive(true);
        yield return null;
    }

    IEnumerator HandleClear()
    {
        GameManager.Instance.isUIClosed = false;

        int totalMin = (int)GameManager.Instance.totalTime / 60;
        int totalSec = (int)GameManager.Instance.totalTime % 60;

        clearText.text = stage.ToString() + "�ܰ� Ŭ���� �����մϴ�!\n\n" +
            totalMin.ToString() + " : " + totalSec.ToString("D2") + "\n\n" +
            "ã�� ����� �� : " + GameManager.Instance.totalCatCount.ToString() + "����";

        popUpPanel.SetActive(true);
        clearText.gameObject.SetActive(true);
        nextStageBtn.SetActive(true);
        yield return null;
    }

    IEnumerator HandleFinalClear()
    {
        GameManager.Instance.isUIClosed = false;

        int totalMin = (int)GameManager.Instance.totalTime / 60;
        int totalSec = (int)GameManager.Instance.totalTime % 60;

        finalClearText.text = "�����մϴ�! ��� Ŭ�����߽��ϴ�!!\n\n" +
            totalMin.ToString() + " : " + totalSec.ToString("D2") + "\n\n" +
            "ã�� ����� �� : " + GameManager.Instance.totalCatCount.ToString() + "����";

        popUpPanel.SetActive(true);
        finalClearText.gameObject.SetActive(true);
        restartBtn.SetActive(true);
        yield return null;
    }

    public void OnRestartButtonClicked()
    {
        Destroy(GameManager.Instance.gameObject);
        Destroy(SoundManager.Instance.gameObject);
        SceneManager.LoadScene("GameStart");
    }

    public void OnNextStageButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
