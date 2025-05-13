using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PrologueManager : MonoBehaviour
{
    //���ѷα� ��� �˾�
    public GameObject prologuePanel;
    public TMP_Text prologue;   //���ѷα� ��� �ؽ�Ʈ
    public Button nextButton;   //���ѷα� ���� ��� ��ư
    public Button startButton;  //���� ���� ��ư

    void Start()
    {
        prologuePanel.SetActive(true);
        startButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(NextPrologue);
        startButton.onClick.AddListener(StartGame);
    }


    void NextPrologue()
    {
        nextButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);

        prologue.text = "��� ��\n���� ���ƿ� ����\n����̰� �� ������ �Ⱥ��δ�!\n�� ���� �� �ɱ�?";
    }

    void StartGame()
    {
        SceneManager.LoadScene("Stage 1");
    }

}
