using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource sound;
    public AudioClip playerJumpSound;

    private float defaultSoundVolume = 0.1f;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    public void playSound(AudioClip clip)
    {
        Debug.Log("playsound");
        sound.volume = 0.05f;
        sound.clip = clip;
        sound.PlayOneShot(clip);
        sound.volume = 0.1f;
    }
}
