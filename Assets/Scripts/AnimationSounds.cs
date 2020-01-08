using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AnimationSounds : MonoBehaviour
{
    [SerializeField] Sounds[] animationSound;

    void playMove1()
    {
        AudioManager.playThisNow(animationSound[0]);
    }

    void playMove2()
    {
        AudioManager.playThisNow(animationSound[1]);
    }

    void playMove3()
    {
        AudioManager.playThisNow(animationSound[2]);
    }

    void playMove4()
    {
        AudioManager.playThisNow(animationSound[3]);
    }

    void playMove5()
    {
        AudioManager.playThisNow(animationSound[4]);
    }

    void playCursor1()
    {
        AudioManager.playThisNow(animationSound[5]);
    }

    void playCursor2()
    {
        AudioManager.playThisNow(animationSound[6]);
    }

    void playCorrect1()
    {
        AudioManager.playThisNow(animationSound[7]);
    }

    void playCorrect2()
    {
        AudioManager.playThisNow(animationSound[8]);
    }

    void playFalse1()
    {
        AudioManager.playThisNow(animationSound[9]);
    }

    void playFalse2()
    {
        AudioManager.playThisNow(animationSound[10]);
    }

    void playApplause()
    {
        AudioManager.playThisNow(animationSound[11]);
    }
}
