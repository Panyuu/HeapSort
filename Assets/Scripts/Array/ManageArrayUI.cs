using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* author: Leon Portius
 * 
 */

public class ManageArrayUI : MonoBehaviour {
    // singleton
    public static ManageArrayUI MAUI;

    public GameObject box;
    public GameObject boxParent;
    public Sprite boxSortedSprite;

    static List<int> arrList = new List<int>();
    // speed of the manual animation 
    float arrBoxDelta = 0.5f;
    static float quickFrameDelta = .05f;
    public static List<GameObject> prefabList = new List<GameObject>();

    static List<GameObject> getPrefabList() {
        return prefabList;
    }

    // get list content from submit button
    public static void setArrList(List<int> heapList) {
        arrList = heapList;
        generateBoxes();
    }
    static List<int> getArrList() {
        return arrList;
    }

    // Vector3 list containing all the possible positions for the array boxes while the animation is running
    static List<Vector3> boxPositions = new List<Vector3>
    {
        new Vector3( -14, 16, 0),
        new Vector3( -10, 16, 0),
        new Vector3(  -6, 16, 0),
        new Vector3(   -2, 16, 0),
        new Vector3(   2, 16, 0),
        new Vector3(  6, 16, 0),
        new Vector3(  10, 16, 0),
        new Vector3(14, 16, 0)
    };
    static List<Vector3> getBoxPosition() {
        return boxPositions;
    }

    // instantiate the singleton
    private void Awake() {
        MAUI = this;
    }

    // generate boxes
    public static void generateBoxes() {
        // for however many boxes exist
        for (byte i = 0; i <= getArrList().Count; i++) {
            // if this is the last box -> the cache box
            if (i == getArrList().Count) {
                prefabList.Add(Instantiate(MAUI.box, getBoxPosition()[i] / 2, Quaternion.identity, MAUI.boxParent.transform));
                prefabList[i].GetComponentsInChildren<TextMesh>()[0].text = " ";
                prefabList[i].GetComponentsInChildren<TextMesh>()[1].text = "Cache";
            }
            else {
                // normal boxes
                prefabList.Add(Instantiate(MAUI.box, getBoxPosition()[i] / 2, Quaternion.identity, MAUI.boxParent.transform));
                prefabList[i].GetComponentsInChildren<TextMesh>()[0].text = getArrList()[i].ToString();
                prefabList[i].GetComponentsInChildren<TextMesh>()[1].text = i.ToString();
            }
        }
    }

   // manual down movement/animation
    public static void moveBoxDown(int parent, int child) {
        getPrefabList()[parent].transform.Translate(new Vector3(0, -MAUI.arrBoxDelta / 15, 0));
        getPrefabList()[child].transform.Translate(new Vector3(0, -MAUI.arrBoxDelta / 15, 0));
    }

    // manual up movement/animation
    public static void moveBoxUpABit(int parent, int child) {
        getPrefabList()[parent].transform.Translate(new Vector3(0, +MAUI.arrBoxDelta / 15, 0));
        getPrefabList()[child].transform.Translate(new Vector3(0, +MAUI.arrBoxDelta / 15, 0));
    }

    // change the text values of parent and child
    public static IEnumerator changeText(int parent, int child) {
        // move the boxes defined as parent and child up fully
        for (byte i = 0; i < 62; i++) {
            moveBoxUpABit(parent, child);
            yield return new WaitForSeconds(quickFrameDelta);
        }

        // save their text values
        string valueA = getPrefabList()[parent].GetComponentsInChildren<TextMesh>()[0].text;
        string valueB = getPrefabList()[child].GetComponentsInChildren<TextMesh>()[0].text;

        // change their text values
        getPrefabList()[parent].GetComponentsInChildren<TextMesh>()[0].text = valueB;
        getPrefabList()[child].GetComponentsInChildren<TextMesh>()[0].text = valueA;

        yield return new WaitForSeconds(.3f);

        // move the boxes defined as parent and child down fully
        for (byte i = 0; i < 62; i++) {
            yield return new WaitForSeconds(quickFrameDelta);
            moveBoxDown(parent, child);
        }

        yield return null;
    }

    //call wenn number is sorted and switch out the box sprite from blue to green
    public static IEnumerator changeSpriteOnceSorted(int index) {
        yield return new WaitForSeconds(0.6f);
        getPrefabList()[index].GetComponent<SpriteRenderer>().sprite = MAUI.boxSortedSprite;
    }
}