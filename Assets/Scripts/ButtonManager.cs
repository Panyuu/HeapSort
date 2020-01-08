using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{

    public GameObject orientaField;
    public GameObject parent;
    public GameObject InputCanvas;

    public float distance;

    private int fieldCount = 1;
    public void addField()
    {
        if (fieldCount < 7)
        { 
            GameObject help = Instantiate(orientaField, new Vector3(orientaField.transform.localPosition.x + distance * fieldCount, orientaField.transform.localPosition.y, orientaField.transform.localPosition.z), Quaternion.identity);
            help.transform.SetParent(parent.transform, false);
            help.name = "Nr" + fieldCount;
            help.transform.GetChild(0).name = "Number" + fieldCount;
            help.transform.GetComponent<TextMeshProUGUI>().SetText("Nr " + (fieldCount+1));
            help.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = fieldCount.ToString();
            //InputCanvas.GetComponent<GetNumberInput>().inFields.SetValue(help,fieldCount);

            fieldCount++;
        }
    }

    public void removeField()
    {
        if (fieldCount>1)
        {
            Destroy(GameObject.Find("Nr" + --fieldCount));
        }
    }




}
