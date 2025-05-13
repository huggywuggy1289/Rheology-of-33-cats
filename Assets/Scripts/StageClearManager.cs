using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageClearManager : MonoBehaviour
{
    public GameObject stageClearCanvas; //ĵ����
    public TMP_Text clearText;  //Ŭ���� ���� �ؽ�Ʈ
    public TMP_Text timeText;   //�ɸ� �ð� �ؽ�Ʈ
    public TMP_Text catInfoText;    //ã�� ����� �� �ؽ�Ʈ
    public Button nextStageButton;  //���� �ܰ� ��ư

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

        // ���������� ���� ��ü Ÿ�̸� �� ���
        if (stageNum <= 2)
            totalTime = 20f;
        else if (stageNum <= 4)
            totalTime = 30f;
        else
            totalTime = 45f;

        float timeTaken = totalTime - timeLeft;
        int min = (int)(timeTaken / 60);
        int sec = (int)(timeTaken % 60);

        clearText.text = $"{stageNum} �ܰ� Ŭ����\n�����մϴ� !";
        timeText.text = $"{min:D2}�� {sec:D2}��";
        catInfoText.text = $"ã�� ����� �� :  {foundCats}����";

    }

    //���� �ܰ� ��ư ������ ���� �������� �ε�
    void GoToNextStage()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
