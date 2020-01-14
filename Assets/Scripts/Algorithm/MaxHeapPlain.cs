using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MaxHeapPlain : MonoBehaviour {

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

    // starts the algorithm when button was pressed
    public static void startMaxHeapPerButtonPress() {
        getValueForStatistic.StartTime = System.DateTime.Now.Millisecond;
        createArray(GetNumberInput.getListForHeap().ToArray());

        // rearranges elements to max-heap (all parents > their children)
        buildHeap();

        while (arrayLength > 0) {

            arrayLength--;

            // save last element in cache, set root to last position -> free space at root
            int lastLeaf = arrayToSort[arrayLength];
            arrayToSort[arrayLength] = arrayToSort[root];

            // moves up largest child to fill up free space, until free space is at leaf-position
            free = downHeap(root);

            // insert cache element, reassure heap property (from bottom up)
            upHeap(free, lastLeaf);
        }

        //Debug.Log("Plain: " + arrayToString());
        getValueForStatistic.EndTime = System.DateTime.Now.Millisecond;
        getValueForStatistic.makeStatisticsTexts();
    }

    // initialize array to be sorted, asigns length to variable
    public static void createArray(int[] array) {
        arrayToSort = array;
        arrayLength = arrayToSort.Length;
        root = 0;
    }

    // build max heap -> find first parent element, then examine children. (if parent < child -> position change)
    // then go to next parent element (reverse BFS)
    public static void buildHeap() {
        for (int parent = (arrayLength / 2 - 1); parent >= 0; parent--) {

            heapify(parent);
        }
    }

    // assort elements in max-heap structure
    public static void heapify(int parent) {
        int child = parent * 2 + 1;

        // while child elements exist
        while (child < arrayLength) {
            // if right child exists and it's larger than left -> use for comparison with parent
            if (child + 1 < arrayLength) {
                getValueForStatistic.Comparison++;

                if (arrayToSort[child + 1] > arrayToSort[child])
                {
                    child++;
                    getValueForStatistic.Comparison++;
                }
                
            }

            // if child > parent -> position change
            if (arrayToSort[parent] >= arrayToSort[child])
            {
                getValueForStatistic.Comparison++;
                return;
            }

            changePosition(parent, child);
            parent = child;
            child = parent * 2 + 1;
        }
    }

    // moves up largest child element to empty space
    public static int downHeap(int parent) {
        int child = parent * 2 + 1;

        while (child + 1 < arrayLength) {

            if (arrayToSort[child + 1] > arrayToSort[child])
            {
                getValueForStatistic.Comparison++;
                child++;
            }
            
            // move up largest child to its parents' position (free space)
            arrayToSort[parent] = arrayToSort[child];

            // continue with next child (if existant)
            parent = child;
            child = parent * 2 + 1;

        }
        // case if there is only one child element
        if (child < arrayLength) {

            arrayToSort[parent] = arrayToSort[child];
            parent = child;

        }

        return parent;
    }

    // insert missing element (cache element) at free space and "sort up" -> compare with parent object, change if needed
    public static void upHeap(int child, int missingElement) {
        int parent;
        arrayToSort[child] = missingElement;

        while (child > root) {
            parent = (child - 1) / 2;
            getValueForStatistic.Comparison++;
            if (arrayToSort[parent] >= arrayToSort[child])
            {
                
                return;
            }

            changePosition(parent, child);
            child = parent;
        }
    }

    // change value of elements at position a and b
    public static void changePosition(int a, int b) {

        int help = arrayToSort[a];
        arrayToSort[a] = arrayToSort[b];
        arrayToSort[b] = help;
        getValueForStatistic.Switches++;
    }

    // testing purpose
    public static string arrayToString() {
        string array = "";

        for (int i = 0; i < arrayToSort.Length; i++) {

            array += arrayToSort[i] + ", ";
        }

        return array;
    }

}
