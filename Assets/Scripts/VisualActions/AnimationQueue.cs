using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationQueue : MonoBehaviour
{
    public static Queue<IEnumerator> animQueue;

    public static AnimationQueue aq;

    private void Awake() {

        aq = this;
        animQueue = new Queue<IEnumerator>();
    }

    public static IEnumerator startAnimation() {


        int count = 0;

        while (true) {
            if (animQueue.Count != 0) {

                Debug.Log(++count + "animations executed");
                yield return aq.StartCoroutine(animQueue.Dequeue());


            }

            yield return new WaitForSeconds(2f);
        }
    }
}
