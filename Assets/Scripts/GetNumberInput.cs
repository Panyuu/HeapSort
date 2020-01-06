using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GetNumberInput : MonoBehaviour
{
       // turn into list for dynmic inputfield generation
       // per knopfdruck inputfields enablen und nicht im script generieren
    [SerializeField] TMPro.TMP_InputField[] inFields;

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


    public void printOutInputFields()
    {
        
        foreach(TMPro.TMP_InputField i in inFields)
        {
            
            if (!(i.text.Equals(string.Empty)))
            {
                
                int temp = 0;
                if(Regex.IsMatch(i.text, @"^\d+$"))
                {
                    temp = int.Parse(i.text);
                    print(temp);
                    if (temp <101 && temp > -1)
                    {
                        print("temp: " + temp);
                        getListForHeap().Add(temp);
                    }
                    
                }
            }
        }

        printOutList();
    }

    private void printOutList()
    {


        foreach (int i in getListForHeap())
        {
            print(i + "    " + getListForHeap().Count + "!");
        }

        MaxHeap.startMaxHeapPerButtonPress();
    }

}

