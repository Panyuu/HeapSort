using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHeapVisual : MonoBehaviour {

    /* Ablauf des Algorithmus:
     * 1. Max-Heap bilden -> Vaterknoten >= Kindknoten && linker Kindknoten >= rechter Kindknoten (von Blatt zur Wurzel vorarbeiten)
     * 2. letztes Element speichern, Wurzelwert an letzte Stelle schreiben, dann von oben nach unten gehen und immer größten Wert nach oben schreiben
     * und an unterster Stelle Element wieder einfügen
     * 3. Max-Heap bilden (von eingefügtem Element nach oben arbeiten, Rest ist schon sortiert)
     * 4. Wiederhole 2. und 3., bis ganzes Array sortiert ist (Größe des Heaps = 0)
     */

    public static int[] arrayToSort;
    public static int arrayLength, root, free;
    public static Queue<IEnumerator> animQueue;

    public static MaxHeap mh;

    private void Awake() {

        
        animQueue = new Queue<IEnumerator>();
    }

    // starts the algorithm when button was pressed
    public static IEnumerator startVisualMaxHeapPerButtonPress() {

        arrayToSort = GetNumberInput.getListForHeap().ToArray();
        arrayLength = arrayToSort.Length;
        root = 0;

        Debug.Log("übergebenes Array: " + arrayToString());

        VisualHeap.positionShips(arrayToSort);

        for (int parent = (arrayLength / 2 - 1); parent >= 0; parent--) {

            int child = parent * 2 + 1;

            // Solange Kindknoten existieren ...
            while (child < arrayLength) {
                // Wenn es rechten Kindknoten gibt und dieses größer ist als das linke, dann wird dieses weiter betrachtet.
                if (child + 1 < arrayLength) {

                    if (arrayToSort[child + 1] > arrayToSort[child]) {

                        child++;
                    }
                    else {

                    }
                }
                else {

                }


                // Wenn Kind größer ist als Elternknoten, dann Positionswechsel
                if (arrayToSort[parent] >= arrayToSort[child]) {
                    Debug.Log("hi");
                }
                else {

                    yield return VisualHeap.ChangeShipPosition(parent, child);

                    Debug.Log("Parent at Index: " + parent + " = " + arrayToSort[parent] + " child at index: " + child + " = " + arrayToSort[child]);

                    changePosition(parent, child);
                    parent = child;
                    child = parent * 2 + 1;
                }
            }
        }

        while (arrayLength > 0) {
            arrayLength--;
            // Letztes Element merken (vorerst nicht im Array enthalten) und Wurzelelement (größtes Element) an letzte Stelle schreiben.
            // -> an Wurzelstelle wird Platz frei
            yield return VisualHeap.WriteRootToLast(root, arrayLength - 1);
            //VisualHeap.writeRootToLast(root, arrayLength);
            int lastLeaf = arrayToSort[arrayLength];
            arrayToSort[arrayLength] = arrayToSort[root];
            // rückt Elemente nach und speichert sich Ort der freien Stelle (letzter freier Platz im übrigen Array).
            int parent = 0;
            int child = 0 * 2 + 1;

            // Da rechtes Kind größer ist als linkes, wird bevorzugt dieses betrachtet.
            while (child + 1 < arrayLength) {

                if (arrayToSort[child + 1] > arrayToSort[child]) {
                    child++;
                }
                else {

                }

                yield return VisualHeap.moveUp(parent, child);

                arrayToSort[parent] = arrayToSort[child];
                parent = child;
                child = parent * 2 + 1;
            }
            // Fall, dass kein rechtes aber ein linkes Kind existiert.
            if (child < arrayLength) {
                arrayToSort[parent] = arrayToSort[child];
                parent = child;
            }

            // letztes Element, das vorher aus Array gelöscht wurde, wird wieder eingefügt und Heap-Eigenschaft wird wieder geprüft (von unten nach oben).
            yield return VisualHeap.writeCacheBack(free);
            //VisualHeap.writeCacheBack(free);
            upHeap(free, lastLeaf);
            arrayToSort[child] = lastLeaf;

            while (child > root) {
                parent = (child - 1) / 2;
                if (arrayToSort[parent] >= arrayToSort[child]) {
                    
                }

                yield return VisualHeap.ChangeShipPosition(parent, child);
                changePosition(parent, child);
                child = parent;
            }
        }
    }

    // Fügt fehlendes Element an freier Stelle ein und sortiert dann von unten nach oben, bis Max-Heap-Struktur wieder eingehalten.
    public static void upHeap(int child, int missingElement) {

    }

    // Tauscht Wert an Index-Stelle a und b miteinander.
    public static void changePosition(int a, int b) {

        //VisualHeap.changeShipPosition(a, b);
        int help = arrayToSort[a];
        arrayToSort[a] = arrayToSort[b];
        arrayToSort[b] = help;
    }

    public static string arrayToString() {
        string array = "";

        for (int i = 0; i < arrayToSort.Length; i++) {

            array += arrayToSort[i] + ", ";
        }

        return array;
    }
}
