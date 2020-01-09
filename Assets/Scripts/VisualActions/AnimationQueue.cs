using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationQueue : MonoBehaviour
{
    //public static Queue<IEnumerator> animQueue;

    public static AnimationQueue aq;

    private void Awake() {

        aq = this;
        //animQueue = new Queue<IEnumerator>();
    }

    public static IEnumerator animate(Queue<IEnumerator> animQueue) {

        while (true) {

            if (animQueue.Count != 0) {

                IEnumerator play = animQueue.Dequeue();

                NewMethod(play);
            }

        }
    }

    private static void NewMethod(IEnumerator play) {
        aq.StartCoroutine(play);
    }
}
