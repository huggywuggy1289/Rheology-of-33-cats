using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        bgmSlider.value = SoundManager.Instance.bgmPlayer.volume;
        sfxSlider.value = SoundManager.Instance.sfxPlayer.volume;
    }

    private void OnEnable()
    {
        GameManager.Instance.isUIClosed = false;
    }

    private void Update()
    {
        ChangeVolume();
    }

    private void ChangeVolume()
    {
        SoundManager.Instance.SetBGMVolume(bgmSlider.value);
        SoundManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    public void OnRestartButtonClicked()
    {
        Destroy(GameManager.Instance.gameObject);
        Destroy(SoundManager.Instance.gameObject);
        Time.timeScale = 1;
        SceneManager.LoadScene("GameStart");
    }

    public void OnFullScreenBtnClicked()
    {
        GameManager.Instance.SetResolution();
    }

    public void OnSettingBtnClicked()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnCloseButtonClicked()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.isUIClosed = true;
    }
}
