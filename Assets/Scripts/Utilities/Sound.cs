using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public bool loop;

    [Range(0f, 1f)]
    public float volume;

    [Range(-3f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public static bool operator true(Sound sound)
    {
        return sound != null;
    }
    public static bool operator false(Sound sound)
    {
        return sound != null;
    }
    public static bool operator !(Sound sound)
    {
        return sound == null;
    }
}
