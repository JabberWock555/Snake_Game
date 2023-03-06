using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Singleton Script
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //-------

    public bool Mute = false;
    [Range(0, 1)]
    public float Volume = 1f;

    public AudioSource SoundEffect;
    public AudioSource Music;

    public SoundType[] Sounds;

    private void Start()
    {
        PlayMusic(SoundEvents.BackgroundMusic);
    }

    private void Update()
    {

       AudioSetting(Volume, Mute);

    }

    public void PlayMusic(SoundEvents sound)
    {

        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {

            Music.clip = clip;
            Music.Play();
            Music.volume = 0.7f;
        }
        else
        {
            Debug.LogError("No Clip found for the event");
        }
    }

    public void Play(SoundEvents sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            SoundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Clip found for the event");
        }
    }


    private void AudioSetting(float volume, bool status)
    {
        if (status)
        {
            Music.volume = 0;
            SoundEffect.volume = 0;
        }
        else
        {
            Music.volume = volume;
            SoundEffect.volume = volume;
        }
    }

    private AudioClip getSoundClip(SoundEvents sound)
    {
        SoundType Clip = Array.Find(Sounds, i => i.soundType == sound);

        if (Clip != null)
        {
            return Clip.soundClip;
        }
        else
        {
            return null;
        }
    }



}
[Serializable]
public class SoundType
{
    public SoundEvents soundType;
    public AudioClip soundClip;
}

public enum SoundEvents
{
    ButtonClick,
    BackgroundMusic,
    EatApple,
    EatSkull,
    EatPowerup,
    GameOver
}

