﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinHeap : MonoBehaviour {

    /* Ablauf des Algorithmus:
     * 1. Min-Heap bilden -> Vaterknoten <= Kindknoten (von Blatt zur Wurzel hocharbeiten)
     * 2. letztes Element speichern, Wurzelwert an letzte Stelle schreiben, dann von oben nach unten gehen und immer kleinsten Wert nach oben schreiben
     * und an unterster Stelle Element wieder einfügen; nochmal mit Elternknoten vergleichen und ggf. tauschen
     * 4. Array-Länge um 1 verringern und 2. wiederholen, bis ganzes Array sortiert ist (Größe des Heaps = 0)
     */

    public static int[] arrayToSort;
    public static int arrayLength, root;

    public static void startMinHeapPerButtonPress()
    {
        int[] intArr = GetNumberInput.getListForHeap().ToArray();

        createArray(intArr);

        ManipulateProtocolTextFile.clearTextFile();
        ManipulateProtocolTextFile.addParameterToWriteList("Ungeordnetes Array: " + arrayToString());

        buildHeap();

        while (arrayLength > 0)
        {
            arrayLength--;
            // Letztes Element merken (vorerst nicht im Array enthalten) und Wurzelelement (kleinstes Element) an letzte Stelle schreiben.
            // -> an Wurzelstelle wird Platz frei
            int lastLeaf = arrayToSort[arrayLength];
            arrayToSort[arrayLength] = arrayToSort[root];
            // rückt Elemente nach und speichert sich Ort der freien Stelle (letzter freier Platz im übrigen Array).
            int free = downHeap(root);
            // letztes Element, das vorher aus Array gelöscht wurde, wird wieder eingefügt und Heap-Eigenschaft wird wieder geprüft (von unten nach oben).
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

    // Min-Heap bilden -> ersten nicht Blattknoten suchen, dann Kinder untersuchen, ggf. tauschen. 
    // dann zum nächsten Vaterknoten übergehen.
    public static void buildHeap()
    {
        for (int parent = arrayLength / 2 - 1; parent >= 0; parent--)
        {
            heapify(parent);
            //System.out.println(arrayToString());
        }
    }

    // Elemente im Array in Heapstruktur bringen.
    public static void heapify(int parent)
    {
        int child = parent * 2 + 1;

        // Solange Kindknoten existieren ...
        while (child < arrayLength)
        {
            // Wenn es rechten Kindknoten gibt und dieses kleiner ist als das linke, dann wird dieses weiter betrachtet.
            if (child + 1 < arrayLength)
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + ";   Kindknoten_1: " + arrayToSort[child] + ";   Kindknoten_2: " + arrayToSort[child + 1] + ";    ArrayLänge: " + arrayLength);
                
                ManipulateProtocolTextFile.addParameterToWriteList("Vergleich von Kindknoten_1: " + arrayToSort[child] + " mit Kindknoten_2: " + arrayToSort[child + 1]);
                if (arrayToSort[child + 1] < arrayToSort[child])
                {
                    ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " > Kindknoten_2: " + arrayToSort[child + 1]);
                    child++;
                } else
                {
                    ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " <= Kindknoten_2: " + arrayToSort[child + 1]);
                }
            } else
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + ";    Kindknoten_1: " + arrayToSort[child] + ";    ArrayLänge: " + arrayLength);
            }

            // Wenn Kind kleiner ist als Elternknoten, dann Positionswechsel
            ManipulateProtocolTextFile.addParameterToWriteList("Vergleich von Vaterknoten: " + arrayToSort[parent] + " mit Kindknoten: " + arrayToSort[child]);
            if (arrayToSort[parent] <= arrayToSort[child])
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " <= Kindknoten: " + arrayToSort[child] + ".");
                ManipulateProtocolTextFile.addParameterToWriteList("Kein Tausch hat stattgefunden zwischen Vaterknoten: " + arrayToSort[parent] + " und Kindknoten: " + arrayToSort[child] + ".");
                return;
            } else
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " > Kindknoten: " + arrayToSort[child] + ".");
                changePosition(parent, child);
                parent = child;
                child = parent * 2 + 1;
            } 
        }
    }

    // Methode beginnt bei der Wurzel und tauscht das kleinste Kind nach oben an die freie Stelle.
    // Dabei entsteht an einem der unteren Knoten eine freie Stelle, die später befüllt wird.
    public static int downHeap(int parent)
    {
        int child = parent * 2 + 1;

        // So lange es rechte Kindknoten gibt, werden die Kinder miteinander verglichen und das kleinere mit Elternknoten verglichen.
        while (child + 1 < arrayLength)
        {

            if (arrayToSort[child + 1] < arrayToSort[child])
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " > Kindknoten_2: " + arrayToSort[child + 1]);
                child++;
            } else
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Kindknoten_1: " + arrayToSort[child] + " <= Kindknoten_2: " + arrayToSort[child + 1]);

            }

            arrayToSort[parent] = arrayToSort[child];
            parent = child;
            child = parent * 2 + 1;
        }
        // Fall, dass kein rechtes aber ein linkes Kind existiert.
        if (child < arrayLength) {

            arrayToSort[parent] = arrayToSort[child];
            parent = child;
            ManipulateProtocolTextFile.addParameterToWriteList("neuer Vaterknoten: " + arrayToSort[parent] + ".");
        }

        return parent;
    }

    // Fügt fehlendes Element an freier Stelle ein und sortiert dann von unten nach oben, bis Min-Heap-Struktur wieder eingehalten.
    public static void upHeap(int child, int missingElement)
    {
        int parent;
        arrayToSort[child] = missingElement;

        while (child > root)
        {
            parent = (child - 1) / 2;
            if (arrayToSort[parent] <= arrayToSort[child])
            {
                ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " <= Kindknoten: " + arrayToSort[child]);
                return;
            }
            ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[parent] + " > Kindknoten: " + arrayToSort[child]);
            changePosition(parent, child);
            child = parent;
        }
    }

    // Tauscht Wert an Index-Stelle a und b miteinander.
    public static void changePosition(int a, int b)
    {
        ManipulateProtocolTextFile.addParameterToWriteList("Vaterknoten: " + arrayToSort[a] + " wechselt die Position mit Kindknoten: " + arrayToSort[b] + ".   !");
        int help = arrayToSort[a];
        arrayToSort[a] = arrayToSort[b];
        arrayToSort[b] = help;

        ManipulateProtocolTextFile.addParameterToWriteList(arrayToString());
    }

    public static string arrayToString()
    {
        string array = "";

        for (int i = 0; i < arrayToSort.Length; i++) {

            array += arrayToSort[i] + ", ";
        }

        return array;
    }
}
