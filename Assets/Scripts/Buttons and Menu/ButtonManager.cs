using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour {
    public GameObject orientaField;
    public GameObject parent;
    public GameObject InputCanvas;
    public static List<TMP_InputField> inputFields;

    public float distance;

    private int fieldCount = 1;

    private void Start() {

        inputFields = new List<TMP_InputField>();
        inputFields.Add(GameObject.Find("Nr0").gameObject.transform.GetChild(0).GetComponent<TMP_InputField>());
    }


    public void addField() {
        if (fieldCount < 7) {
            GameObject help = Instantiate(orientaField, new Vector3(orientaField.transform.localPosition.x + distance * fieldCount, orientaField.transform.localPosition.y, orientaField.transform.localPosition.z), Quaternion.identity);
            help.transform.SetParent(parent.transform, false);
            help.name = "Nr" + fieldCount;
            help.transform.GetChild(0).name = "Number" + fieldCount;
            help.transform.GetComponent<TextMeshProUGUI>().SetText("Nr " + (fieldCount + 1));
            help.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = fieldCount.ToString();
            help.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = fieldCount.ToString();
            //InputCanvas.GetComponent<GetNumberInput>().inFields.SetValue(help,fieldCount);

            // adds input fields to list 
            inputFields.Add(help.transform.GetChild(0).GetComponent<TMP_InputField>());

            fieldCount++;
        }
    }

    public void removeField() {
        if (fieldCount > 1) {
            //deletes last element in list and destroys object
            inputFields.RemoveAt(inputFields.Count - 1);
            Destroy(GameObject.Find("Nr" + --fieldCount));
        }
    }
}
