using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* generate the amount of boxes = list.Count
 * index stays the same
 * tauschpartner1 font scaled kurz größer, dann wieder kleiner
 * tauschpartner2 font scaled kurz größer, dann wieder kleiner
 * die zahlen werden ""
 * .text von t1 & t2 tauschen
 * if ein element sortiert ändere sprite
 * 
 */

public class ManageArrayUI : MonoBehaviour {
    public static ManageArrayUI MAUI;

    public GameObject box;
    public GameObject boxParent;
    public Sprite boxSortedSprite;

    static List<int> arrList = new List<int>();
    float arrBoxDelta = 0.5f;
    static float quickFrameDelta = .05f;
    public static List<GameObject> prefabList = new List<GameObject>();

    static List<GameObject> getPrefabList() {
        return prefabList;
    }

    // get list content from submit button?
    public static void setArrList(List<int> heapList) {
        arrList = heapList;
        generateBoxes();
    }
    static List<int> getArrList() {
        return arrList;
    }

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

    private void Awake() {
        MAUI = this;
    }

    // generate boxes
    // let them swoop down into view from above 1 after another
    public static void generateBoxes() {
        for (byte i = 0; i <= getArrList().Count; i++) {
            if (i == getArrList().Count) {
                prefabList.Add(Instantiate(MAUI.box, getBoxPosition()[i] / 2, Quaternion.identity, MAUI.boxParent.transform));
                prefabList[i].GetComponentsInChildren<TextMesh>()[0].text = " ";
                prefabList[i].GetComponentsInChildren<TextMesh>()[1].text = "Cache";
            }
            else {

                prefabList.Add(Instantiate(MAUI.box, getBoxPosition()[i] / 2, Quaternion.identity, MAUI.boxParent.transform));
                prefabList[i].GetComponentsInChildren<TextMesh>()[0].text = getArrList()[i].ToString();
                prefabList[i].GetComponentsInChildren<TextMesh>()[1].text = i.ToString();
            }
        }
    }

    // call from changeMethod to change font scaling w/ IEnumerator up to same speed as normal animations
    // make .text values = ""
    // switch their .text values
    public static void moveBoxDown(int parent, int child) {
        getPrefabList()[parent].transform.Translate(new Vector3(0, -MAUI.arrBoxDelta / 15, 0));
        getPrefabList()[child].transform.Translate(new Vector3(0, -MAUI.arrBoxDelta / 15, 0));
    }

    public static void moveBoxUpABit(int parent, int child) {
        getPrefabList()[parent].transform.Translate(new Vector3(0, +MAUI.arrBoxDelta / 15, 0));
        getPrefabList()[child].transform.Translate(new Vector3(0, +MAUI.arrBoxDelta / 15, 0));
    }


    public static IEnumerator changeText(int parent, int child) {
        // up fully
        for (byte i = 0; i < 62; i++) {
            moveBoxUpABit(parent, child);
            yield return new WaitForSeconds(quickFrameDelta);
        }

        string valueA = getPrefabList()[parent].GetComponentsInChildren<TextMesh>()[0].text;
        string valueB = getPrefabList()[child].GetComponentsInChildren<TextMesh>()[0].text;

        getPrefabList()[parent].GetComponentsInChildren<TextMesh>()[0].text = valueB;
        getPrefabList()[child].GetComponentsInChildren<TextMesh>()[0].text = valueA;

        yield return new WaitForSeconds(.3f);

        for (byte i = 0; i < 62; i++) {
            yield return new WaitForSeconds(quickFrameDelta);
            moveBoxDown(parent, child);
        }

        yield return null;
    }

    //call wenn number sorted switch out sprite
    public static IEnumerator changeSpriteOnceSorted(int index) {
        yield return new WaitForSeconds(0.6f);
        getPrefabList()[index].GetComponent<SpriteRenderer>().sprite = MAUI.boxSortedSprite;
    }
}