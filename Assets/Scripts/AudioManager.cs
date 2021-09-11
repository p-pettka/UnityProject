using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource m_audio;
    private AudioClip backgroundMusic;

    private void PlayBackgroundMusic()
    {
        backgroundMusic = GameplayManager.Instance.GameDatabase.BackgroudMusic;
        m_audio.clip = backgroundMusic;
        m_audio.playOnAwake = true;
        m_audio.loop = true;
        m_audio.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        PlayBackgroundMusic();
    }
}
