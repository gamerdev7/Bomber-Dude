using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume = 1f;

    [Range(-3, 3)]
    public float pitch = 1f;

    public bool loop;
}
