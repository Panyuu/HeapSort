using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinHeap : MonoBehaviour {

    /*
     * algorithm procedure :
     * 1. build min heap (heapify) -> parent element <= child elemets
     * 2. save last element in cache variable, write root element to last position in array (creates empty/free space at root position)
     * -> root element now in right position (no need to look at it anymore)
     * 3. fill up empty space at root with smallest child, do so until free space is at leaf-position
     * 4. fill empty space with element in cache variable and build min-heap again (in general: check if cache element < parent element
     * 5. decrease unsorted array length by one
     * 6. repeat 2.-5. until array is completely sorted (arraylength = 0)
     */
    
    // stores the array
    public static int[] arrayToSort;
    // stores current length of array to be looked at, root element's position (0) and position of empty space in heap
    public static int arrayLength, root, free;

    // needed for visual heap
    public static Queue<IEnumerator> animQueue;
    public static Queue<int> parameters;

    // singleton
    public static MinHeap mh;

    private void Awake() {

        mh = this;

        // initialize queues
        animQueue = new Queue<IEnumerator>();
        parameters = new Queue<int>();

        // starts visual heap coroutine
        mh.StartCoroutine(startAnimation());
    }

    // starts the algorithm when button was pressed
    public static void startMinHeapPerButtonPress() {

        createArray(GetNumberInput.getListForHeap().ToArray());
        //createArray( new int[] { 1, 3, 6, 4, 2, 5, 7 } );

        animQueue.Enqueue(VisualHeap.positionShips(GetNumberInput.getListForHeap().ToArray()));
        //animQueue.Enqueue(VisualHeap.positionShips(new int[] { 1, 3, 6, 4, 2, 5, 7}));

        ManipulateProtocolTextFile.clearTextFile();
        ManipulateProtocolTextFile.addParameterToWriteList("Ungeordnetes Array: " + arrayToString());

        // rearranges elements to min-heap (all parents < their children)
        buildHeap();

        while (arrayLength > 0) {

            arrayLength--;

            // save last element in cache, set root to last position -> free space at root
            parameters.Enqueue(root);
            parameters.Enqueue(arrayLength);
            animQueue.Enqueue(VisualHeap.WriteRootToLast(parameters.Dequeue(), parameters.Dequeue()));
            int lastLeaf = arrayToSort[arrayLength];
            arrayToSort[arrayLength] = arrayToSort[root];

            // moves up smallest child to fill up free space, until free space is at leaf-position
            free = downHeap(root);

            // insert cache element, reassure heap property (from bottom up)
            parameters.Enqueue(free);
            animQueue.Enqueue(VisualHeap.writeCacheBack(parameters.Dequeue()));
            upHeap(free, lastLeaf);

            ManipulateProtocolTextFile.addParameterToWriteList(arrayToString());
        }

        ManipulateProtocolTextFile.addParameterToWriteList("Geordnetes Array: " + arrayToString());
        ManipulateProtocolTextFile.printOutProtocolContent();
    }

    // initialize array to be sorted, asigns length to variable
    public static void createArray(int[] array) {
        arrayToSort = array;
        arrayLength = arrayToSort.Length;
        root = 0;
    }

    // build min heap -> find first parent element, then examine children. (if parent > child -> position change)
    // then go to next parent element (reverse BFS)
    public static void buildHeap() {
        for (int parent = (arrayLength / 2 - 1); parent >= 0; parent--) {

            heapify(parent);
        }
    }

    // assort elements in min-heap structure
    public static void heapify(int parent) {
        int child = parent * 2 + 1;

        // while child elements exist
        while (child < arrayLength) {
            // if right child exists and it's smaller than left -> use for comparison with parent
            if (child + 1 < arrayLength) {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + ";    Kindknoten_1: " + arrayToSort[child] + ";    Kindknoten_2: " + arrayToSort[child + 1] + ";    ArrayLänge: " + arrayLength);

                ManipulateProtocolTextFile.addParameterToWriteList("Vergleich von Kindknoten_1: " + arrayToSort[child] + " mit Kindknoten_2: " + arrayToSort[child + 1]);
                if (arrayToSort[child + 1] < arrayToSort[child]) {
                    // put ring around child objects (find larger)
                    parameters.Enqueue(child + 1);
                    parameters.Enqueue(child);
                    parameters.Enqueue(loliSpawnPoint(child, child + 1));
                    parameters.Enqueue(0);
                    animQueue.Enqueue(VisualHeap.findLargerElement(parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue()));

                    ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " < Kindknoten_2: " + arrayToSort[child + 1]);

                    child++;
                }
                // if not smaller use use left one
                else {
                    // put rings around child objects (find larger)
                    parameters.Enqueue(child);
                    parameters.Enqueue(child + 1);
                    parameters.Enqueue(loliSpawnPoint(child, child + 1));
                    parameters.Enqueue(180);
                    animQueue.Enqueue(VisualHeap.findLargerElement(parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue()));

                    ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " >= Kindknoten_2: " + arrayToSort[child + 1]);
                }
            }
            // if right doesn't exist use left child
            else {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + ";    Kindknoten_1: " + arrayToSort[child] + ";    ArrayLänge: " + arrayLength);
            }

            // if child < parent -> position change
            ManipulateProtocolTextFile.addParameterToWriteList("Vergleich von Vaterknoten: " + arrayToSort[parent] + " mit Kindknoten: " + arrayToSort[child]);
            if (arrayToSort[parent] <= arrayToSort[child]) {

                // puts rings around two elements -> here: no position change
                parameters.Enqueue(parent);
                parameters.Enqueue(child);
                parameters.Enqueue(loliSpawnPoint(parent, child));
                if (parent * 2 + 1 == child) {
                    parameters.Enqueue(0);
                }
                else {
                    parameters.Enqueue(180);
                }
                animQueue.Enqueue(VisualHeap.noSwitchNecessary(parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue()));

                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " >= Kindknoten: " + arrayToSort[child] + ".");
                ManipulateProtocolTextFile.addParameterToWriteList("Kein Tausch hat stattgefunden zwischen Vaterknoten: " + arrayToSort[parent] + " und Kindknoten: " + arrayToSort[child] + ".");
                return;
            }
            else {

                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " < Kindknoten: " + arrayToSort[child] + ".");

                changePosition(parent, child);
                parent = child;
                child = parent * 2 + 1;
            }
        }
    }

    // moves up smallest child element to empty space
    public static int downHeap(int parent) {
        int child = parent * 2 + 1;

        while (child + 1 < arrayLength) {

            if (arrayToSort[child + 1] < arrayToSort[child]) {
                // build ring around compared objects
                parameters.Enqueue(child + 1);
                parameters.Enqueue(child);
                parameters.Enqueue(loliSpawnPoint(child, child + 1));
                parameters.Enqueue(0);
                animQueue.Enqueue(VisualHeap.findLargerElement(parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue()));

                ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " < Kindknoten_2: " + arrayToSort[child + 1]);
                child++;
            }
            else {
                // build ring around compared objects
                parameters.Enqueue(child);
                parameters.Enqueue(child + 1);
                parameters.Enqueue(loliSpawnPoint(child, child + 1));
                parameters.Enqueue(180);
                animQueue.Enqueue(VisualHeap.findLargerElement(parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue()));

                ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " >= Kindknoten_2: " + arrayToSort[child + 1]);
            }

            // move up smallest child to it's parents position (free space)
            parameters.Enqueue(parent);
            parameters.Enqueue(child);
            animQueue.Enqueue(VisualHeap.moveUp(parameters.Dequeue(), parameters.Dequeue()));
            arrayToSort[parent] = arrayToSort[child];

            // continue with next child (if existant)
            parent = child;
            child = parent * 2 + 1;

            ManipulateProtocolTextFile.addParameterToWriteList("neuer Vaterknoten: " + arrayToSort[parent]);
        }
        // case if there is only one child element
        if (child < arrayLength) {
            parameters.Enqueue(parent);
            parameters.Enqueue(child);
            animQueue.Enqueue(VisualHeap.moveUp(parameters.Dequeue(), parameters.Dequeue()));
            arrayToSort[parent] = arrayToSort[child];
            parent = child;
            ManipulateProtocolTextFile.addParameterToWriteList("neuer Vaterknoten: " + arrayToSort[parent] + ".");
        }

        ManipulateProtocolTextFile.addParameterToWriteList("Der jetzt zu betrachtende Vaterknoten: " + arrayToSort[parent]);
        return parent;
    }

    // insert missing element (cache element) at free space and "sort up" -> compare with parent object, change if needed
    public static void upHeap(int child, int missingElement) {
        int parent;
        arrayToSort[child] = missingElement;

        while (child > root) {
            parent = (child - 1) / 2;
            if (arrayToSort[parent] <= arrayToSort[child]) {
                // build rings around objects
                parameters.Enqueue(parent);
                parameters.Enqueue(child);
                parameters.Enqueue(loliSpawnPoint(parent, child));
                if (parent * 2 + 1 == child) {
                    parameters.Enqueue(0);
                }
                else {
                    parameters.Enqueue(180);
                }
                animQueue.Enqueue(VisualHeap.noSwitchNecessary(parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue()));

                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " >= Kindknoten: " + arrayToSort[child]);
                return;
            }
            ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " < Kindknoten: " + arrayToSort[child]);
            changePosition(parent, child);
            child = parent;
        }
    }

    // change value of elements at position a and b
    public static void changePosition(int a, int b) {
        // for rings and the compare sign
        parameters.Enqueue(a);
        parameters.Enqueue(b);
        parameters.Enqueue(loliSpawnPoint(a, b));
        if (a * 2 + 1 == b) {

            parameters.Enqueue(180);
        }
        else {
            parameters.Enqueue(0);
        }
        animQueue.Enqueue(VisualHeap.ChangeShipPosition(parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue(), parameters.Dequeue()));
        ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[a] + " wechselt die Position mit Kindknoten: " + arrayToSort[b] + ".   !");
        int help = arrayToSort[a];
        arrayToSort[a] = arrayToSort[b];
        arrayToSort[b] = help;

        ManipulateProtocolTextFile.addParameterToWriteList(arrayToString());
    }

    // determines where the compare sign has to "spawn" -> depending on which subtree is inspected
    public static int loliSpawnPoint(int inspectedIndex1, int inspectedIndex2) {

        int loliPos = 1;

        // right sub tree
        if (inspectedIndex1 == 2 || inspectedIndex1 == 5 && inspectedIndex2 == 6) {

            loliPos = 2;
        }
        // left sub tree
        else if (inspectedIndex1 == 0 || inspectedIndex1 == 1 && inspectedIndex2 == 2) {

            loliPos = 0;
        }
        return loliPos;
    }

    // testing purpose
    public static string arrayToString() {
        string array = "";

        for (int i = 0; i < arrayToSort.Length; i++) {

            array += arrayToSort[i] + ", ";
        }

        return array;
    }

    // starts the visual heap-transformation
    public static IEnumerator startAnimation() {


        int count = 0;

        while (true) {
            if (animQueue.Count != 0) {

                Debug.Log(++count + "animations executed");
                yield return mh.StartCoroutine(animQueue.Dequeue());
            }

            yield return new WaitForSeconds(4f);
        }
    }
}
