using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHeap : MonoBehaviour {

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

        mh = this;
        animQueue = new Queue<IEnumerator>();
    }

    // starts the algorithm when button was pressed
    public static void startMaxHeapPerButtonPress() {
        
        int[] intArr = GetNumberInput.getListForHeap().ToArray();

        mh.StartCoroutine(startAnimation());

        createArray(intArr);

        animQueue.Enqueue(VisualHeap.positionShips(arrayToSort));

        //mh.StartCoroutine(VisualHeap.positionShips(intArr));

        ManipulateProtocolTextFile.clearTextFile();
        ManipulateProtocolTextFile.addParameterToWriteList("Ungeordnetes Array: " + arrayToString());

        buildHeap();

        while (arrayLength > 0) {
            arrayLength--;
            // Letztes Element merken (vorerst nicht im Array enthalten) und Wurzelelement (größtes Element) an letzte Stelle schreiben.
            // -> an Wurzelstelle wird Platz frei
            animQueue.Enqueue(VisualHeap.WriteRootToLast(root, arrayLength - 1));
            //VisualHeap.writeRootToLast(root, arrayLength);
            int lastLeaf = arrayToSort[arrayLength];
            arrayToSort[arrayLength] = arrayToSort[root];
            // rückt Elemente nach und speichert sich Ort der freien Stelle (letzter freier Platz im übrigen Array).
            free = downHeap(root);
            // letztes Element, das vorher aus Array gelöscht wurde, wird wieder eingefügt und Heap-Eigenschaft wird wieder geprüft (von unten nach oben).
            animQueue.Enqueue(VisualHeap.writeCacheBack(free));
            //VisualHeap.writeCacheBack(free);
            upHeap(free, lastLeaf);

            ManipulateProtocolTextFile.addParameterToWriteList(arrayToString());
        }

        ManipulateProtocolTextFile.addParameterToWriteList("Geordnetes Array: " + arrayToString());
        ManipulateProtocolTextFile.printOutProtocolContent();
    }

    // Inizialisierung des zu sortierenden Arrays, sowie Speicherung von dessen Länge.
    public static void createArray(int[] array)
    {
        arrayToSort = array;
        arrayLength = arrayToSort.Length;
        root = 0;
    }

    // 1. Max-Heap bilden -> ersten nicht Blattknoten suchen, dann Kinder untersuchen, ggf. tauschen. 
    // dann zum nächsten Vaterknoten übergehen.
    public static void buildHeap()
    {
        for (int parent = (arrayLength / 2 - 1); parent >= 0; parent--)
        {
            Debug.Log("Vater: " + parent + " = " + arrayToSort[parent]);
            Debug.Log(arrayToString());
            heapify(parent);
            
            //ManipulateProtocolTextFile.addParameterToWriteList(arrayToString());
        }
    }

    // Elemente im Array in Heapstruktur bringen.
    public static void heapify(int parent)
    {
        int child = parent * 2 + 1;

        // Solange Kindknoten existieren ...
        while (child < arrayLength)
        {
            // Wenn es rechten Kindknoten gibt und dieses größer ist als das linke, dann wird dieses weiter betrachtet.
            if (child + 1 < arrayLength)
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + ";    Kindknoten_1: " + arrayToSort[child] + ";    Kindknoten_2: " + arrayToSort[child + 1] + ";    ArrayLänge: " + arrayLength);

                ManipulateProtocolTextFile.addParameterToWriteList("Vergleich von Kindknoten_1: " + arrayToSort[child] + " mit Kindknoten_2: " + arrayToSort[child + 1]);
                if (arrayToSort[child + 1] > arrayToSort[child])
                {
                    ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " < Kindknoten_2: " + arrayToSort[child + 1]);
                    child++;
                } 
                else
                {
                    ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " >= Kindknoten_2: " + arrayToSort[child + 1]);
                }
            } else
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + ";    Kindknoten_1: " + arrayToSort[child] + ";    ArrayLänge: " + arrayLength);
            }


            // Wenn Kind größer ist als Elternknoten, dann Positionswechsel
            ManipulateProtocolTextFile.addParameterToWriteList("Vergleich von Vaterknoten: " + arrayToSort[parent] + " mit Kindknoten: " + arrayToSort[child]);
            if (arrayToSort[parent] >= arrayToSort[child]) {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " >= Kindknoten: " + arrayToSort[child] + ".");
                ManipulateProtocolTextFile.addParameterToWriteList("Kein Tausch hat stattgefunden zwischen Vaterknoten: " + arrayToSort[parent] + " und Kindknoten: " + arrayToSort[child] + ".");
                return;
            }
            else {

                
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " < Kindknoten: " + arrayToSort[child] + ".");
                // ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " wechselt die Position mit Kindknoten: " + arrayToSort[child]);
                animQueue.Enqueue(VisualHeap.ChangeShipPosition(parent, child));

                Debug.Log("Parent at Index: " + parent + " = " + arrayToSort[parent] + " child at index: " + child + " = " + arrayToSort[child]);

                changePosition(parent, child);
                parent = child;
                child = parent * 2 + 1;
            }
        }
    }

    // Methode beginnt bei der Wurzel und tauscht das größte Kind (rechts, wenn vorhanden) nach oben an die freie Stelle.
    // Dabei entsteht an einem der unteren Knoten eine freie Stelle, die später befüllt wird.
    public static int downHeap(int parent)
    {
        int child = parent * 2 + 1;

        // Da rechtes Kind größer ist als linkes, wird bevorzugt dieses betrachtet.
        while (child + 1 < arrayLength)
        {

            if (arrayToSort[child + 1] > arrayToSort[child])
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " < Kindknoten_2: " + arrayToSort[child + 1]);
                child++;
            } else
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " >= Kindknoten_2: " + arrayToSort[child + 1]);
            }

            animQueue.Enqueue(VisualHeap.moveUp(parent, child));
            //VisualHeap.moveUp(parent, child);
            arrayToSort[parent] = arrayToSort[child];
            parent = child;
            child = parent * 2 + 1;
            ManipulateProtocolTextFile.addParameterToWriteList("neuer Vaterknoten: " + arrayToSort[parent]);
        }
        // Fall, dass kein rechtes aber ein linkes Kind existiert.
        if (child < arrayLength)
        {
            arrayToSort[parent] = arrayToSort[child];
            parent = child;
            ManipulateProtocolTextFile.addParameterToWriteList("neuer Vaterknoten: " + arrayToSort[parent] + ".");
        }
        
        ManipulateProtocolTextFile.addParameterToWriteList("Der jetzt zu betrachtende Vaterknoten: " + arrayToSort[parent]);
        return parent;
    }

    // Fügt fehlendes Element an freier Stelle ein und sortiert dann von unten nach oben, bis Max-Heap-Struktur wieder eingehalten.
    public static void upHeap(int child, int missingElement)
    {
        int parent;
        arrayToSort[child] = missingElement;

        while (child > root)
        {
            parent = (child - 1) / 2;
            if (arrayToSort[parent] >= arrayToSort[child]) {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " >= Kindknoten: " + arrayToSort[child]);
                return;
            }
            ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " < Kindknoten: " + arrayToSort[child]);
            changePosition(parent, child);
            child = parent;
        }
    }

    // Tauscht Wert an Index-Stelle a und b miteinander.
    public static void changePosition(int a, int b)
    {

        animQueue.Enqueue(VisualHeap.ChangeShipPosition(a, b));
        //VisualHeap.changeShipPosition(a, b);
        ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[a] + " wechselt die Position mit Kindknoten: " + arrayToSort[b] + ".   !");
        int help = arrayToSort[a];
        arrayToSort[a] = arrayToSort[b];
        arrayToSort[b] = help;
        
        ManipulateProtocolTextFile.addParameterToWriteList(arrayToString());
    }

    public static string arrayToString()
    {
        string array = "";

        for (int i = 0; i < arrayToSort.Length; i++)
        {

            array += arrayToSort[i] + ", ";
        }

        return array;
    }

    public static IEnumerator startAnimation() {


        int count = 0;

        while (true) {
            if (animQueue.Count != 0) {

                Debug.Log(++count + "animations executed");
                yield return mh.StartCoroutine(animQueue.Dequeue());


            }

            yield return new WaitForSeconds(2f);
        }
    }
}
