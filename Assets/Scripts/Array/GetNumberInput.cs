using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GetNumberInput : MonoBehaviour
{
    [SerializeField] GameObject inputCanvas;
    [SerializeField] GameObject buttonCanvas;
    // turn into list for dynmic inputfield generation
    [SerializeField] TMPro.TMP_InputField[] inFields;
    // per knopfdruck inputfields enablen und nicht im script generieren

    //list that saves converted input from input canvas
    static List<int> listForHeap = new List<int>();

    //getter method for listForHeap
    public static List<int> getListForHeap() {
        return listForHeap;
    }

    void Start()
    {

        foreach (TMPro.TMP_InputField iField in inFields)
        {
            // method that happens when input is entered
            iField.onEndEdit.AddListener(SubmitNumber);
        }
    }
    
    // method that's called when something has been entered into an input field
    private void SubmitNumber(string num)
    {
        Debug.Log(num);
    }

    //more precise name would be 'extract infomation & forward'
    // extract input from input fields and save it into listForHeap, which gets transfered to Heap Algorithm
    private void extractInputForHeap()
    {
        inFields = ButtonManager.inputFields.ToArray();

        //cycle through all the input fields
        foreach (TMPro.TMP_InputField i in inFields)
        {
            // are they empty?
            if (!(i.text.Equals(string.Empty)))
            {
                // temporal variable to save input into different data type
                int temp = 0;
                // use Regular expression to check if the string 
                if(Regex.IsMatch(i.text, @"^\d+$"))
                {
                    // cast the string into int
                    temp = int.Parse(i.text);
                    // print out for console.log
                    print(temp);
                    // only allow numbers bewteen 0 & 100
                    if (temp <100 && temp > -1)
                    {
                        //// print out for console.log
                        //print("temp: " + temp);
                        //// add the valid input to the heap list
                        getListForHeap().Add(temp);
                    }
                }
            }
        }
        // 
        ManageArrayUI.setArrList(getListForHeap());
        
    }

    //final printout & start max algorithm
    public void printOutListMax()
    {
        extractInputForHeap();

        // prints out every element in list
        //foreach (int i in getListForHeap())
        //{
        //    print(i + "    " + getListForHeap().Count + "!");
        //}
        //starts the max Heap
        MaxHeap.startMaxHeapPerButtonPress();
        MaxHeapPlain.startMaxHeapPerButtonPress();
        
        inputCanvas.SetActive(false);
        buttonCanvas.SetActive(true);
    }

    //final printout & start in algorithm
    public void printOutListMin() 
    {
        extractInputForHeap();

        // prints out every element in list
        //foreach (int i in getListForHeap())
        //{
        //    print(i + "    " + getListForHeap().Count + "!");
        //}
        //starts the max Heap
        MinHeap.startMinHeapPerButtonPress();

        inputCanvas.SetActive(false);
        buttonCanvas.SetActive(true);
    }
}

