using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    private AudioSource backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();

        backgroundMusic.clip = GameplayManager.Instance.GameDatabase.BackgroudMusic;
        backgroundMusic.playOnAwake = true;
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }
}
