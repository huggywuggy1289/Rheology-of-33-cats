using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageClearManager : MonoBehaviour
{
    public GameObject stageClearCanvas; //캔버스
    public TMP_Text clearText;  //클리어 축하 텍스트
    public TMP_Text timeText;   //걸린 시간 텍스트
    public TMP_Text catInfoText;    //찾은 고양이 수 텍스트
    public Button nextStageButton;  //다음 단계 버튼

    void Start()
    {
        stageClearCanvas.SetActive(false);
        nextStageButton.onClick.AddListener(GoToNextStage);
    }

    public void ShowStageClear(float timeLeft)
    {
        stageClearCanvas.SetActive(true);

        int stageNum = GameManager.Instance.currentStageNum;
        int foundCats = GameManager.Instance.stageCatCount;
        float totalTime;

        // 스테이지에 따라 전체 타이머 값 계산
        if (stageNum <= 2)
            totalTime = 20f;
        else if (stageNum <= 4)
            totalTime = 30f;
        else
            totalTime = 45f;

        float timeTaken = totalTime - timeLeft;
        int min = (int)(timeTaken / 60);
        int sec = (int)(timeTaken % 60);

        clearText.text = $"{stageNum} 단계 클리어\n축하합니다 !";
        timeText.text = $"{min:D2}분 {sec:D2}초";
        catInfoText.text = $"찾은 고양이 수 :  {foundCats}마리";

    }

    //다음 단계 버튼 누르면 다음 스테이지 로드
    void GoToNextStage()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
