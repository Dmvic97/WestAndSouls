using UnityEngine;

public class AudioMainMenu : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip titleMusic;
    public AudioClip playButton;
    public AudioClip optionsButton;

    private void Start()
    {
        musicSource.clip = titleMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
