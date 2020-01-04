using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetNumberInput : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField[] inFields;
    List<int> listForHeap = new List<int>();

    public List<int> getListForHeap() {
        return listForHeap;
    }

    void Start()
    {
        foreach (TMPro.TMP_InputField iField in inFields)
        {
            iField.onEndEdit.AddListener(SubmitNumber);
        }
    }
    
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
                print(i.text);
                int temp = 0;
                if(int.TryParse(i.ToString(), out temp))
                {
                    if(temp <101 && temp > -1)
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
            print(i + "    " + getListForHeap().Count);
        }
    }

}

