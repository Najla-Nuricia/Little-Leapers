
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip gemSFX;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip checkpointSFX;
    [SerializeField] private AudioClip deathSFX;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
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

    private void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgm != null)
        {
            bgmSource.clip = bgm;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlayGemSFX()
    {
        sfxSource.PlayOneShot(gemSFX);
    }

    public void PlayJumpSFX()
    {
        sfxSource.PlayOneShot(jumpSFX);
    }

    public void PlayCheckpointSFX()
    {
        sfxSource.PlayOneShot(checkpointSFX);
    }

    public void PlayDeathSFX()
    {
        sfxSource.PlayOneShot(deathSFX);
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
