using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GetNumberInput : MonoBehaviour
{
    [SerializeField] GameObject inputCanvas;
    [SerializeField] GameObject buttonCanvas;
    // turn into list for dynmic inputfield generation
    [SerializeField] TMPro.TMP_InputField[] inFields;
    // enable inputfields at button press, not in script

    //list that saves converted input from input canvas
    public static List<int> listForHeap = new List<int>();

    //getter method for listForHeap
    public static List<int> getListForHeap() {
        return listForHeap;
    }

    //more precise name would be 'extract infomation & forward'
    // extract input from input fields and save it into listForHeap, which gets transfered to Heap Algorithm
    private void extractInputForHeap()
    {
        inFields = ButtonManager.inputFields.ToArray();

        //cycle through all the input fields
        foreach (TMPro.TMP_InputField i in inFields) {
            // are they empty?
            if (!(i.text.Equals(string.Empty))) {
                // temporal variable to save input into different data type
                int temp = 0;
                // use Regular expression to check if the string 
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
        ManageArrayUI.setArrList(getListForHeap());    
    }

    //final printout & start max algorithm
    public void printOutListMax()
    {
        extractInputForHeap();

        if (getListForHeap().Count == 0)
        {
            CallStatistics.callStatisticAfterVisualization();
        }
        inputCanvas.SetActive(false);
        buttonCanvas.SetActive(true);

        //starts the max Heap
        MaxHeap.startMaxHeapPerButtonPress();
        MaxHeapPlain.startMaxHeapPerButtonPress();  
    }

    //final printout & start in algorithm
    public void printOutListMin() 
    { 
        extractInputForHeap();

        if (getListForHeap().Count == 0)
        {
            CallStatistics.callStatisticAfterVisualization();
        }
        //starts the max Heap
        MinHeap.startMinHeapPerButtonPress();
        MinHeapPlain.startMinHeapPerButtonPress();

        inputCanvas.SetActive(false);
        buttonCanvas.SetActive(true);
    }
}

