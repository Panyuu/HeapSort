using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// author: Leon Portius
public class GetNumberInput : MonoBehaviour {

    public GameObject inputCanvas;
    public GameObject buttonCanvas;
    // turn into list for dynmic inputfield generation
    public TMPro.TMP_InputField[] inFields;

    //list that saves converted input from input canvas
    public static List<int> listForHeap = new List<int>();

    //getter method for listForHeap
    public static List<int> getListForHeap() {
        return listForHeap;
    }

    //more precise name would be 'extract infomation & forward'
    // extract input from input fields and save it into listForHeap, which gets transfered to Heap Algorithm
    private void extractInputForHeap() {

        inFields = ButtonManager.inputFields.ToArray();

        //cycle through all the input fields
        foreach (TMPro.TMP_InputField i in inFields) {
            // are they empty?
            if (!(i.text.Equals(string.Empty))) {
                // temporal variable to save input into different data type
                int temp = 0;
                // use Regular expression to check if the string is an integer although unity does that check as well
                if (Regex.IsMatch(i.text, @"^\d+$")) {
                    // cast the string into int
                    temp = int.Parse(i.text);
                    // only allow numbers bewteen 0 & 100
                    if (temp < 100 && temp > -1) {
                        // add the valid input to the heap list
                        getListForHeap().Add(temp);
                        GetValueForStatistic.OwnArr.Add(temp);
                    }
                }
            }
        }
        // pass on the valid input to the UI manager
        ManageArrayUI.setArrList(getListForHeap());
    }

    //final printout & start max algorithm
    public void printOutListMax() {
        extractInputForHeap();

        // if there was no valid input
        if (getListForHeap().Count == 0) {
            CallStatistics.callStatisticAfterVisualization();
        }
        // turn the input canvas off & the buttons in the animation on
        inputCanvas.SetActive(false);
        buttonCanvas.SetActive(true);

        //starts the max Heaps
        MaxHeap.startMaxHeapPerButtonPress();
        MaxHeapPlain.startMaxHeapPerButtonPress();
    }

    //final printout & start in min algorithm
    public void printOutListMin() {
        extractInputForHeap();

        // if there was no valid input
        if (getListForHeap().Count == 0) {
            CallStatistics.callStatisticAfterVisualization();
        }
        //starts the mix Heap
        MinHeap.startMinHeapPerButtonPress();
        MinHeapPlain.startMinHeapPerButtonPress();

        // turn the input canvas off & the buttons in the animation on
        inputCanvas.SetActive(false);
        buttonCanvas.SetActive(true);
    }

    // clear values
    public static void setBackEverything() {

        listForHeap.Clear();
    }
}
