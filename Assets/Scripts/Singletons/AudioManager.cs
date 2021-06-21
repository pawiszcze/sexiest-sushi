using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource soundSource;
    public AudioSource musicSource;
    public AudioClip playerJumpSound, backgroundMusic;

    private float defaultSoundVolume = 0.1f;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void playSound(AudioClip clip)
    {
        Debug.Log("playsound");
        soundSource.clip = clip;
        soundSource.PlayOneShot(clip);
        soundSource.volume = 0.1f;
    }

    public void playMusic(AudioClip music, float musicVolume)
    {
        musicSource.volume = musicVolume;
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }
}
