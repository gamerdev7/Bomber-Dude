using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] musicClips;
    [SerializeField] private Sound[] sfxClips;

    private AudioSource bgMusicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Initialize();

        PlayMusic("Theme1");
    }

    private void Initialize()
    {
        bgMusicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        bgMusicSource.loop = true;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicClips, sound => sound.name == name);

        if (s == null) { return; }

        bgMusicSource.clip = s.clip;
        bgMusicSource.volume = s.volume;
        bgMusicSource.pitch = s.pitch;
        bgMusicSource.loop = s.loop;
        bgMusicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxClips, sound => sound.name == name);

        if (s == null) { return; }

        sfxSource.PlayOneShot(s.clip, 1);
        sfxSource.volume = s.volume;
        sfxSource.pitch = s.pitch;
        sfxSource.loop = s.loop;
    }
}
