using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{


    public Sounds[] sounds;

    // Start is called before the first frame update
    void Awake()
    {

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
           
        }
        //sounds[0].source.loop = true;
        //sounds[1].source.loop = true;
    }

   
    public void Play (string name)
    {
        Sounds s = System.Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }

    private void Start()
    {
       // Play("music");
        //Play("waves");
    }
    /*
    public static void playThisNow(AnimationClip s)
    {
        s.source.Play();
    }
    */

    public void Playmove1()
    {
        Play("move1");
    }

    public void Playmove2()
    {
        Play("move2");
    }


    public void Playmove3()
    {
        Play("move3");
    }

    public void Playmove4()
    {
        Play("move4");
    }

    public void Playmove5()
    {
        Play("move5");
    }



}
