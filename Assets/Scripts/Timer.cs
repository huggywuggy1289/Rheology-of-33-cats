using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    public TMP_Text[] timeText;       // [0]=Min, [1]=Colon, [2]=Sec
    public TMP_Text gameOverText;
    public TMP_Text clearText;
    public TMP_Text finalClearText;   // ← 최종 클리어용 메시지

    float time;
    int min, sec;
    bool timerStopped = false;

    void Start()
    {
        int stage = GameManager.Instance.currentStageNum;

        // 스테이지별 시간 설정
        if (stage <= 2)
            time = 20f;
        else if (stage <= 4)
            time = 30f;
        else
            time = 45f;

        min = (int)time / 60;
        sec = (int)time % 60;

        timeText[0].text = min.ToString("D2");
        timeText[2].text = sec.ToString("D2");

        gameOverText.gameObject.SetActive(false);
        clearText.gameObject.SetActive(false);
        finalClearText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timerStopped) return;

        time -= Time.deltaTime;

        min = (int)time / 60;
        sec = (int)time % 60;

        if (time <= 0f)
        {
            time = 0f;
            timeText[0].text = "00";
            timeText[2].text = "00";
            StartCoroutine(HandleGameOver());
            timerStopped = true;
        }
        else
        {
            timeText[0].text = min.ToString("D2");
            timeText[2].text = sec.ToString("D2");

            // 고양이 다 찾은 경우
            int stage = GameManager.Instance.currentStageNum;
            if (GameManager.Instance.stageCatCount >= GameManager.Instance.catNumToFind[stage])
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
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
    }

    IEnumerator HandleClear()
    {
        clearText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Stage " + (GameManager.Instance.currentStageNum + 1));
    }

    IEnumerator HandleFinalClear()
    {
        finalClearText.gameObject.SetActive(true);
        yield break;
    }
}
