using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// author: Esra Poetter, Leon Portius

public class AudioManager : MonoBehaviour {
    // array for easy sound file access
    public Sounds[] sounds;

    
    void Awake() {
        foreach (Sounds s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // play the sound via parameter
    public void Play(string name) {
        Sounds s = System.Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }

    // play move 1 when animationEvent calls for it
    public void Playmove1() {
        Play("move1");
    }

    // play move 2 when animationEvent calls for it
    public void Playmove2() {
        Play("move2");
    }

    // play move 3 when animationEvent calls for it
    public void Playmove3() {
        Play("move3");
    }
    // play move 4 when animationEvent calls for it
    public void Playmove4() {
        Play("move4");
    }
    // play move 5 when animationEvent calls for it
    public void Playmove5() {
        Play("move5");
    }
    // play applause when animationEvent calls for it
    public void Playapplause() {
        Play("applause");
    }
}