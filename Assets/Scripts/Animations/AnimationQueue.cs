using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationQueue : MonoBehaviour
{
    public static Queue<string> animQueue;

    public static AnimationQueue aq;

    private void Awake() {

        aq = this;
        animQueue = new Queue<string>();
    }

    public static IEnumerator animate() {

        while (true) {

            if (animQueue.Count != 0) {

                string action = animQueue.Dequeue();

                if (action.Equals("positionShips")) {

                    aq.StartCoroutine(VisualHeap.positionShips(MaxHeap.arrayToSort));
                }
                if (action.Equals("writeRootToLast")) {

                    aq.StartCoroutine(VisualHeap.WriteRootToLast(MaxHeap.root, MaxHeap.arrayLength));
                }
                if (action.Equals("writeCacheBack")) {

                    aq.StartCoroutine(VisualHeap.writeCacheBack(MaxHeap.free));
                }
                if (action.Equals("moveUp")) {

                    aq.StartCoroutine(VisualHeap.moveUp(MaxHeap.child, MaxHeap.parent));
                }
                if (action.Equals("changeShipPosition")) {

                    aq.StartCoroutine(VisualHeap.ChangeShipPosition(MaxHeap.parent, MaxHeap.child));
                }
            }
        
        }
    }
}
