using UnityEngine.Audio;
using UnityEngine;

// author: Esra Poetter, Leon Portius

[System.Serializable]
public class Sounds {

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
