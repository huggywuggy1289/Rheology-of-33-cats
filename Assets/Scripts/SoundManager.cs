using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource bgmPlayer;
    public AudioSource sfxPlayer;
    [SerializeField] private AudioClip bgm_clip;
    [SerializeField] private AudioClip[] sfx_clip;

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

        bgmPlayer = GameObject.Find("BGMPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();

        bgmPlayer.volume = .2f;
        sfxPlayer.volume = .5f;

        // 게임 시작 버튼을 누르면 BGM이 시작되는 것으로 변경 예정
        PlayBGMSound();
    }

    public void PlayBGMSound()
    {
        bgmPlayer.clip = bgm_clip;
        bgmPlayer.Play();
    }

    public void PlaySFXSound()
    {
        int randomNum = Random.Range(0, sfx_clip.Length);
        sfxPlayer.clip = sfx_clip[randomNum];
        sfxPlayer.PlayOneShot(sfxPlayer.clip);
    }

    public void SetBGMVolume(float volume)
    {
        if (volume != sfxPlayer.volume)
        {
            bgmPlayer.volume = volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            if (volume != sfxPlayer.volume)
            {
                sfxPlayer.volume = volume;
                PlaySFXSound();
            }
        }
    }
}
