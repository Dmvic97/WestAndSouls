using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip shot;
    public AudioClip enemyShot;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip walk;
    public AudioClip enemyDeath;
    public AudioClip trap;
    public AudioClip playerHurt;
    public AudioClip bulletImpact;
    public AudioClip bulletImpactGround;
    public AudioClip gameOverScreen;
    public AudioClip heal;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    
}
