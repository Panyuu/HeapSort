using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MaxHeap : MonoBehaviour {

    /*
     * algorithm procedure :
     * 1. build max heap (heapify) -> parent element >= child elemets
     * 2. save last element in cache variable, write root element to last position in array (creates empty/free space at root position)
     * -> root element now in right position (no need to look at it anymore)
     * 3. fill up empty space at root with largest child, do so until free space is at leaf-position
     * 4. fill empty space with element in cache variable and build max-heap again (in general: check if cache element < parent element
     * 5. decrease unsorted array length by one
     * 6. repeat 2.-5. until array is completely sorted (arraylength = 0)
     */

    // stores the array
    public static int[] arrayToSort;
    // stores current length of array to be looked at, root element's position (0) and position of empty space in heap
    public static int arrayLength, root, free;

    public GameObject protoText;
    public GameObject textHolder;
    public static int protoCount;

    // needed for visual heap
    public static List<IEnumerator> animQueue;
    public static List<int> parameters;
    public static List<int> stindex;
    public static bool playAnimation;
    public static int indexAni, testIndexAni;

    

    // singleton
    public static MaxHeap mh;
    //public static ButtonManager bm; 
    private void Awake() {

        mh = this;


        // initialize queues
        animQueue = new List<IEnumerator>();
        parameters = new List<int>();
        stindex = new List<int>();
        indexAni = 0;
        testIndexAni = 0;

        playAnimation = true;
        // starts visual heap coroutine
        mh.StartCoroutine(startAnimation());
    }

    // starts the algorithm when button was pressed
    public static void startMaxHeapPerButtonPress() {

        createArray(GetNumberInput.getListForHeap().ToArray());
        //createArray( new int[] { 1, 3, 6, 4, 2, 5, 7 } );

        stindex.Add(-1);
        animQueue.Add(VisualHeap.positionShips(GetNumberInput.getListForHeap().ToArray()));
        //animQueue.Enqueue(VisualHeap.positionShips(new int[] { 1, 3, 6, 4, 2, 5, 7}));


        //mh.StartCoroutine(VisualHeap.positionShips(intArr));

        ManipulateProtocolTextFile.clearTextFile();
        ManipulateProtocolTextFile.addParameterToWriteList("Ungeordnetes Array: " + arrayToString());
        protofill("Ungeordnetes Array: " + arrayToString(),indexAni);

        // rearranges elements to max-heap (all parents > their children)
        buildHeap();

        while (arrayLength > 0) {

            arrayLength--;

            // save last element in cache, set root to last position -> free space at root
            stindex.Add(indexAni);
            parameters.Add(root);
            parameters.Add(arrayLength);
            animQueue.Add(VisualHeap.WriteRootToLast(parameters[indexAni++], parameters[indexAni++]));
            int lastLeaf = arrayToSort[arrayLength];
            arrayToSort[arrayLength] = arrayToSort[root];

            // moves up largest child to fill up free space, until free space is at leaf-position
            free = downHeap(root);

            // insert cache element, reassure heap property (from bottom up)
            parameters.Add(free);
            animQueue.Add(VisualHeap.writeCacheBack(parameters[indexAni++]));
            upHeap(free, lastLeaf);

            ManipulateProtocolTextFile.addParameterToWriteList(arrayToString());
        }

        ManipulateProtocolTextFile.addParameterToWriteList("Geordnetes Array: " + arrayToString());
        protofill("Geordnetes Array: " + arrayToString(),indexAni);
        ManipulateProtocolTextFile.printOutProtocolContent();
    }

    // initialize array to be sorted, asigns length to variable
    public static void createArray(int[] array)
    {
        arrayToSort = array;
        arrayLength = arrayToSort.Length;
        root = 0;
    }

    // build max heap -> find first parent element, then examine children. (if parent < child -> position change)
    // then go to next parent element (reverse BFS)
    public static void buildHeap()
    {
        for (int parent = (arrayLength / 2 - 1); parent >= 0; parent--)
        {

            heapify(parent);
        }
    }

    // assort elements in max-heap structure
    public static void heapify(int parent)
    {
        int child = parent * 2 + 1;

        // while child elements exist
        while (child < arrayLength)
        {
            // if right child exists and it's larger than left -> use for comparison with parent
            if (child + 1 < arrayLength)
            {
                stindex.Add(indexAni);
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + ";    Kindknoten_1: " + arrayToSort[child] + ";    Kindknoten_2: " + arrayToSort[child + 1] + ";    ArrayLänge: " + arrayLength);
                protofill("Vaterknoten: " + arrayToSort[parent] + ";    Kindknoten_1: " + arrayToSort[child] + ";    Kindknoten_2: " + arrayToSort[child + 1] + ";    ArrayLänge: " + arrayLength,indexAni);
                ManipulateProtocolTextFile.addParameterToWriteList("Vergleich von Kindknoten_1: " + arrayToSort[child] + " mit Kindknoten_2: " + arrayToSort[child + 1]);
                protofill("Vergleich von Kindknoten_1: " + arrayToSort[child] + " mit Kindknoten_2: " + arrayToSort[child + 1],indexAni);
                if (arrayToSort[child + 1] > arrayToSort[child])
                {

                    // put ring around child objects (find larger)
                    parameters.Add(child + 1);
                    parameters.Add(child);
                    parameters.Add(loliSpawnPoint(child, child + 1));
                    parameters.Add(180);
                    animQueue.Add(VisualHeap.findLargerElement(parameters[indexAni++], parameters[indexAni++], parameters[indexAni++], parameters[indexAni++]));

                    ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " < Kindknoten_2: " + arrayToSort[child + 1]);
                    protofill("Kindknoten_1: " + arrayToSort[child] + " < Kindknoten_2: " + arrayToSort[child + 1],indexAni);

                    child++;
                }
                // if not larger use use left one
                else
                {
                    // put rings around child objects (find larger)
                    parameters.Add(child);
                    parameters.Add(child + 1);
                    parameters.Add(loliSpawnPoint(child, child + 1));
                    parameters.Add(0);
                    animQueue.Add(VisualHeap.findLargerElement(parameters[indexAni++], parameters[indexAni++], parameters[indexAni++], parameters[indexAni++]));

                    ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " >= Kindknoten_2: " + arrayToSort[child + 1]);
                    protofill("Kindknoten_1: " + arrayToSort[child] + " >= Kindknoten_2: " + arrayToSort[child + 1],indexAni);
                }
            }
            // if right doesn't exist use left child
            else
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + ";    Kindknoten_1: " + arrayToSort[child] + ";    ArrayLänge: " + arrayLength);
                protofill("Vaterknoten: " + arrayToSort[parent] + ";    Kindknoten_1: " + arrayToSort[child] + ";    ArrayLänge: " + arrayLength,indexAni);
            }

            // if child > parent -> position change
            ManipulateProtocolTextFile.addParameterToWriteList("Vergleich von Vaterknoten: " + arrayToSort[parent] + " mit Kindknoten: " + arrayToSort[child]);
            protofill("Vergleich von Vaterknoten: " + arrayToSort[parent] + " mit Kindknoten: " + arrayToSort[child],indexAni);
            if (arrayToSort[parent] >= arrayToSort[child]) {

                // puts rings around two elements -> here: no position change
                stindex.Add(indexAni);
                parameters.Add(parent);
                parameters.Add(child);
                parameters.Add(loliSpawnPoint(parent, child));
                if (parent * 2 + 1 == child) {
                    parameters.Add(180);
                }
                else {
                    parameters.Add(0);
                }

                animQueue.Add(VisualHeap.noSwitchNecessary(parameters[indexAni++], parameters[indexAni++], parameters[indexAni++], parameters[indexAni++]));

                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " >= Kindknoten: " + arrayToSort[child] + ".");
                protofill("Vaterknoten: " + arrayToSort[parent] + " >= Kindknoten: " + arrayToSort[child] + ".",indexAni);
                ManipulateProtocolTextFile.addParameterToWriteList("Kein Tausch hat stattgefunden zwischen Vaterknoten: " + arrayToSort[parent] + " und Kindknoten: " + arrayToSort[child] + ".");
                protofill("Kein Tausch hat stattgefunden zwischen Vaterknoten: " + arrayToSort[parent] + " und Kindknoten: " + arrayToSort[child] + ".",indexAni);
                return;
            }
            else {

                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " < Kindknoten: " + arrayToSort[child] + ".");
                protofill("Vaterknoten: " + arrayToSort[parent] + " < Kindknoten: " + arrayToSort[child] + ".",indexAni);

                changePosition(parent, child);
                parent = child;
                child = parent * 2 + 1;
            }
        }
    }

    // moves up largest child element to empty space
    public static int downHeap(int parent)
    {
        int child = parent * 2 + 1;

        while (child + 1 < arrayLength)
        {
            stindex.Add(indexAni);
            if (arrayToSort[child + 1] > arrayToSort[child])
            {
                // build ring around compared objects
                parameters.Add(child + 1);
                parameters.Add(child);
                parameters.Add(loliSpawnPoint(child, child + 1));
                parameters.Add(180);
                animQueue.Add(VisualHeap.findLargerElement(parameters[indexAni++], parameters[indexAni++], parameters[indexAni++], parameters[indexAni++]));

                ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " < Kindknoten_2: " + arrayToSort[child + 1]);
                protofill("Kindknoten_1: " + arrayToSort[child] + " < Kindknoten_2: " + arrayToSort[child + 1],indexAni);
                child++;
            } else
            {
                // build ring around compared objects
                parameters.Add(child);
                parameters.Add(child + 1);
                parameters.Add(loliSpawnPoint(child, child + 1));
                parameters.Add(0);
                animQueue.Add(VisualHeap.findLargerElement(parameters[indexAni++], parameters[indexAni++], parameters[indexAni++], parameters[indexAni++]));

                ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " >= Kindknoten_2: " + arrayToSort[child + 1]);
                protofill("Kindknoten_1: " + arrayToSort[child] + " >= Kindknoten_2: " + arrayToSort[child + 1],indexAni);
            }
            stindex.Add(indexAni);
            // move up largest child to it's parents position (free space)
            parameters.Add(parent);
            parameters.Add(child);
            animQueue.Add(VisualHeap.moveUp(parameters[indexAni++], parameters[indexAni++]));
            arrayToSort[parent] = arrayToSort[child];

            // continue with next child (if existant)
            parent = child;
            child = parent * 2 + 1;

            ManipulateProtocolTextFile.addParameterToWriteList("neuer Vaterknoten: " + arrayToSort[parent]);
            protofill("neuer Vaterknoten: " + arrayToSort[parent],indexAni);
        }
        // case if there is only one child element
        if (child < arrayLength)
        {
            stindex.Add(indexAni);
            parameters.Add(parent);
            parameters.Add(child);
            ManipulateProtocolTextFile.addParameterToWriteList("neuer Vaterknoten: " + arrayToSort[parent] + ".");
            protofill("neuer Vaterknoten: " + arrayToSort[parent] + ".", indexAni);
            animQueue.Add(VisualHeap.moveUp(parameters[indexAni++], parameters[indexAni++]));
            arrayToSort[parent] = arrayToSort[child];
            parent = child;
            
        }

        ManipulateProtocolTextFile.addParameterToWriteList("Der jetzt zu betrachtende Vaterknoten: " + arrayToSort[parent]);
        protofill("Der jetzt zu betrachtende Vaterknoten: " + arrayToSort[parent], indexAni);
        return parent;
    }

    // insert missing element (cache element) at free space and "sort up" -> compare with parent object, change if needed
    public static void upHeap(int child, int missingElement)
    {
        int parent;
        arrayToSort[child] = missingElement;

        while (child > root)
        {
            parent = (child - 1) / 2;
            if (arrayToSort[parent] >= arrayToSort[child]) {

                stindex.Add(indexAni);
                parameters.Add(parent);
                parameters.Add(child);
                parameters.Add(loliSpawnPoint(parent, child));
                if (parent * 2 + 1 == child) {
                    parameters.Add(180);
                }
                else {
                    parameters.Add(0);
                }

                animQueue.Add(VisualHeap.noSwitchNecessary(parameters[indexAni++], parameters[indexAni++], parameters[indexAni++], parameters[indexAni++]));
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " >= Kindknoten: " + arrayToSort[child]);
                protofill("Vaterknoten: " + arrayToSort[parent] + " >= Kindknoten: " + arrayToSort[child],indexAni);
                
                return;
            }
            ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " < Kindknoten: " + arrayToSort[child]);
            protofill("Vaterknoten: " + arrayToSort[parent] + " < Kindknoten: " + arrayToSort[child],indexAni);
            changePosition(parent, child);
            child = parent;
        }
    }

    // change value of elements at position a and b
    public static void changePosition(int a, int b)
    {
        // for rings and the compare sign
        stindex.Add(indexAni);
        parameters.Add(a);
        parameters.Add(b);
        parameters.Add(loliSpawnPoint(a, b));
        if (a * 2 + 1 == b) {

            parameters.Add(0);
        }
        else {
            parameters.Add(180);
        }
        animQueue.Add(VisualHeap.ChangeShipPosition(parameters[indexAni++], parameters[indexAni++], parameters[indexAni++], parameters[indexAni++]));

        ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[a] + " wechselt die Position mit Kindknoten: " + arrayToSort[b] + ".   !");
        protofill("Vaterknoten: " + arrayToSort[a] + " wechselt die Position mit Kindknoten: " + arrayToSort[b] + ".   !",indexAni);
        
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
    public static string arrayToString()
    {
        string array = "";

        for (int i = 0; i < arrayToSort.Length; i++)
        {

            array += arrayToSort[i] + ", ";
        }

        return array;
    }

    // starts the visual heap-transformation
    public static IEnumerator startAnimation()
    {

        //playAnimation = true;


        while (playAnimation) {
            Debug.Log("ANIMATION ANIMATION ANIMATION");
            if (testIndexAni < animQueue.Count)
            {
                indexAni = stindex[testIndexAni];
                yield return mh.StartCoroutine(animQueue[testIndexAni++]);
            }

            yield return new WaitForSeconds(4f);
        }
    }

    public static bool getPlayAnimation()
    {
        return playAnimation;
    }

    public static void setPlayAnimation(bool value)
    {
        playAnimation = value;
    }

    public static void protofill(string content, int index)
    {

        GameObject help = Instantiate(mh.protoText, new Vector2(mh.protoText.transform.localPosition.x, mh.protoText.transform.localPosition.y - 50 * protoCount), Quaternion.identity);
        help.transform.SetParent(mh.textHolder.transform, false);
        help.name = index.ToString();
        help.GetComponent<Text>().text = content;
        

    }

    public static void protoClick()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        indexAni = int.Parse(name.Split(' ')[0]);
    }

}
