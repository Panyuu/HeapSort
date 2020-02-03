using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Tina Kiessling, Leon Portius

public class VisualHeap : MonoBehaviour {
    // singleton
    public static VisualHeap vh;

    // Ship Prefab to instantiate + ring object
    public GameObject shipPrefab, ring1, ring2, loli;
    // positions of ships in heap and for rings
    static Vector3[] shipPosition, ringPosition, ringSize, loliPosition;
    // animators of each object
    static Animator[] anim;
    // stores information about the objects that are sorted
    public static GameObject[] shipsToSort;
    // set back rotation to default
    static Quaternion rotation;

    // stores the position of the separated ship (currently not in heap)
    static Vector3 cachePosition;
    // cache
    static GameObject cacheObject;
    // animator from cache
    static Animator cacheAnimator;

    private void Awake() {

        vh = this;
    }

    // Start is called before the first frame update
    void Start() {

        // positions for the ships in heap-structure
        shipPosition = new Vector3[] {
            new Vector3(0, -1f, 3),
            new Vector3(-6, -2.5f, 1),
            new Vector3(6, -2.5f, 1),
            new Vector3(-8, -4.5f, -0.5f),
            new Vector3(-3, -4.5f, -0.5f),
            new Vector3(3, -4.5f, -0.5f),
            new Vector3(8, -4.5f, -0.5f) };

        ringPosition = new Vector3[] {
            new Vector3(0, 3.3f, 1f),
            new Vector3(-5.7f, 1.6f, 0),
            new Vector3(5.3f, 1.6f, 0),
            new Vector3(-6.3f, 0.3f, -3),
            new Vector3(-2.5f, 0.3f, -3),
            new Vector3(2.2f, 0.3f, -3),
            new Vector3(6.2f, 0.3f, -3) };

        ringSize = new Vector3[] {
            new Vector3(1.5f, 1.5f, 1.5f),
            new Vector3(1.3f, 1.3f, 1.3f),
            new Vector3(1.3f, 1.3f, 1.3f),
            new Vector3(1f, 1f, 1f),
            new Vector3(1f, 1f, 1f),
            new Vector3(1f, 1f, 1f),
            new Vector3(1f, 1f, 1f) };

        loliPosition = new Vector3[] {
            new Vector3(0, -9, 1),
            new Vector3(-4.5f, -10.5f, -2.9f),
            new Vector3(4, -10.5f, -2.9f) };

        rotation = new Quaternion(0, 180, 0, 0);
        cachePosition = new Vector3(11, -1f, 2);
    }

    // Creates the heap structure according to the array.
    public static IEnumerator positionShips(int[] arrayToSort) {

        List<GameObject> ships = new List<GameObject>();
        List<Animator> animators = new List<Animator>();

        for (int i = 0; i < arrayToSort.Length; i++) {

            // Create Object Ship
            ships.Add(Instantiate(vh.shipPrefab, shipPosition[i], rotation));
            // sets the number on the sail
            GameObject value = ships[i].transform.Find("Value").gameObject;
            value.GetComponent<TextMesh>().text = arrayToSort[i].ToString();
            // Rename Object
            ships[i].name = "ShipIndex_" + i;

            animators.Add(ships[i].GetComponent<Animator>());

            animators[i].SetBool("isSurfacing", true);
            yield return new WaitForSeconds(1f);
            animators[i].SetBool("isSurfacing", false);
            // write text onto lower display
            DisplayTextOnLowerBoard.setLowerText("Positionierung Schiffe!");
        }
        shipsToSort = ships.ToArray();
        anim = animators.ToArray();
    }

    // changes complete ship object (position, rotation of sail, text on the ship)
    public static IEnumerator ChangeShipPosition(int a, int b, int loliPos, int loliRot) {

        Debug.Log("Parent Value: " + shipsToSort[a].transform.Find("Value").gameObject.GetComponent<TextMesh>().text + " Child Value: " + shipsToSort[a].transform.Find("Value").gameObject.GetComponent<TextMesh>().text);
        // write text onto lower display
        DisplayTextOnLowerBoard.setLowerText("Tausch der Knoten.");

        // set position to children's position
        vh.ring1.transform.position = ringPosition[a];
        vh.ring1.transform.localScale = ringSize[a];
        vh.ring2.transform.position = ringPosition[b];
        vh.ring2.transform.localScale = ringSize[b];

        // put yellow ring around them
        vh.ring1.transform.GetChild(2).gameObject.SetActive(true);
        vh.ring2.transform.GetChild(2).gameObject.SetActive(true);

        // position loli and play animation
        vh.loli.transform.position = loliPosition[loliPos];
        vh.loli.transform.rotation = new Quaternion(0, loliRot, 0, 0);
        vh.loli.GetComponent<Animator>().SetBool("popUp", true);
        yield return new WaitForSeconds(2f);
        vh.loli.GetComponent<Animator>().SetBool("popUp", false);

        // deactivate rings
        vh.ring1.transform.GetChild(2).gameObject.SetActive(false);
        vh.ring2.transform.GetChild(2).gameObject.SetActive(false);

        vh.ring1.transform.GetChild(1).gameObject.SetActive(true);
        vh.ring2.transform.GetChild(1).gameObject.SetActive(true);

        Debug.Log("Ships Position Changed");
        // change array ui during visualisation for text switch
        ManageArrayUI.MAUI.StartCoroutine(ManageArrayUI.changeText(a, b));

        // saves all necessary variables from a
        GameObject objectA = shipsToSort[a];
        Animator animatorA = anim[a];

        // ships submerge
        anim[a].SetBool("isSubmerging", true);
        anim[b].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        anim[a].SetBool("isSubmerging", false);
        anim[b].SetBool("isSubmerging", false);

        // overwrites current values with the ones from other variable
        // saves values of b in a and changes index
        shipsToSort[a].transform.rotation = rotation;
        anim[a] = anim[b];
        shipsToSort[a].transform.position = shipPosition[b];
        shipsToSort[a] = shipsToSort[b];

        // saves values of a in b and changes index
        shipsToSort[b].transform.rotation = rotation;
        anim[b] = animatorA;
        shipsToSort[b].transform.position = shipPosition[a];
        shipsToSort[b] = objectA;

        // lets ships surface on different position
        anim[a].SetBool("isSurfacing", true);
        anim[b].SetBool("isSurfacing", true);
        yield return new WaitForSeconds(4f);
        anim[a].SetBool("isSurfacing", false);
        anim[b].SetBool("isSurfacing", false);

        // loli vanishes again
        vh.loli.GetComponent<Animator>().SetBool("moveDown", true);
        yield return new WaitForSeconds(1f);
        vh.loli.GetComponent<Animator>().SetBool("moveDown", false);

        // rings vanish
        vh.ring1.transform.GetChild(1).gameObject.SetActive(false);
        vh.ring2.transform.GetChild(1).gameObject.SetActive(false);
    }

    // Separates last element from heap and switches position of root element to last element
    public static IEnumerator WriteRootToLast(int root, int lastElement) {

        if (lastElement != 0) {
            // write text onto lower display
            DisplayTextOnLowerBoard.setLowerText("Letztes Element in Cache zwischenspeichern.");
            // last element submerges
            anim[lastElement].SetBool("isSubmerging", true);
            yield return new WaitForSeconds(3.5f);
            anim[lastElement].SetBool("isSubmerging", false);

            // write into cacheObject + adjust components
            shipsToSort[lastElement].transform.position = cachePosition;
            shipsToSort[lastElement].transform.rotation = rotation;
            cacheAnimator = anim[lastElement];
            cacheObject = shipsToSort[lastElement];

            // write text onto lower display
            DisplayTextOnLowerBoard.setLowerText("Root-Element wandert nach unten...");
            // ship from last position now surfacing outside of heap, root element is submerging
            cacheAnimator.SetBool("isSurfacing", true);
            anim[root].SetBool("isSubmerging", true);
            yield return new WaitForSeconds(1.5f);
            // change text
            ManageArrayUI.MAUI.StartCoroutine(ManageArrayUI.changeText(ManageArrayUI.prefabList.Count - 1, lastElement));
            yield return new WaitForSeconds(2f);
            // change text again
            ManageArrayUI.MAUI.StartCoroutine(ManageArrayUI.changeText(root, lastElement));
            yield return new WaitForSeconds(1.5f);
            anim[root].SetBool("isSubmerging", false);
            cacheAnimator.SetBool("isSurfacing", false);

            // save animator of root object
            Animator rootAnimator = shipsToSort[root].GetComponent<Animator>();

            // overwrite lastElement with rootElement
            shipsToSort[root].transform.position = shipPosition[lastElement];
            shipsToSort[root].transform.rotation = rotation;
            anim[lastElement] = anim[root];
            shipsToSort[lastElement] = shipsToSort[root];

            // write back to root object for future
            anim[root] = rootAnimator;

            // root is surfacing at position of lastElement
            anim[lastElement].SetBool("isSurfacing", true);
            yield return new WaitForSeconds(3.5f);
            anim[lastElement].SetBool("isSurfacing", false);
        }
        else {

            anim[lastElement].transform.rotation = rotation;
        }
        // Element leaves the scene / screen
        anim[lastElement].SetBool("isMovingOutOfScene", true);

        // switch the Array Box sprite to "sorted"
        ManageArrayUI.MAUI.StartCoroutine(ManageArrayUI.changeSpriteOnceSorted(lastElement));
        // write text onto lower display
        DisplayTextOnLowerBoard.setLowerText("Der Knoten is sortiert...");

        yield return new WaitForSeconds(1f);
        anim[lastElement].SetBool("isMovingOutOfScene", false);
        // write text onto lower display
        DisplayTextOnLowerBoard.setLowerText("Und fällt weg.");
    }

    // moves largest child upwards to fill the heap back up
    public static IEnumerator moveUp(int parent, int child) {
        // write text onto lower display
        DisplayTextOnLowerBoard.setLowerText("Knoten rücken auf...");

        // child submerges
        anim[child].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(1.5f);
        // switch text in array ui while visualisation is running
        ManageArrayUI.MAUI.StartCoroutine(ManageArrayUI.changeText(parent, child));
        yield return new WaitForSeconds(2f);
        anim[child].SetBool("isSubmerging", false);

        // change position on screen and in array
        shipsToSort[child].transform.position = shipPosition[parent];
        shipsToSort[child].transform.rotation = rotation;
        anim[parent] = anim[child];
        shipsToSort[parent] = shipsToSort[child];

        // child surfaces at parents space
        anim[parent].SetBool("isSurfacing", true);
        yield return new WaitForSeconds(1f);
        anim[parent].SetBool("isSurfacing", false);

    }

    // writes the cache element back to the free space
    public static IEnumerator writeCacheBack(int free) {
        // write text onto lower display
        DisplayTextOnLowerBoard.setLowerText("Cache Element wieder in Heap einfügen.");

        // cache Object is submerging
        cacheAnimator.SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        // switch text while visualisation is running
        ManageArrayUI.MAUI.StartCoroutine(ManageArrayUI.changeText(free, ManageArrayUI.prefabList.Count - 1));
        yield return new WaitForSeconds(2f);
        cacheAnimator.SetBool("isSubmerging", false);

        // change position of cacheObject back to free space in heap
        cacheObject.transform.position = shipPosition[free];
        cacheObject.transform.rotation = rotation;
        anim[free] = cacheAnimator;
        shipsToSort[free] = cacheObject;

        anim[free].SetBool("isSurfacing", true);
        yield return new WaitForSeconds(1f);
        anim[free].SetBool("isSurfacing", false);
    }

    // find larger child element
    public static IEnumerator findLargerElement(int larger, int smaller, int loliPos, int loliRot) {

        //selectTwoElements(larger, smaller);
        // set position to children's position
        vh.ring1.transform.position = ringPosition[larger];
        vh.ring1.transform.localScale = ringSize[larger];
        vh.ring2.transform.position = ringPosition[smaller];
        vh.ring2.transform.localScale = ringSize[smaller];

        // put yellow ring around them
        vh.ring1.transform.GetChild(2).gameObject.SetActive(true);
        vh.ring2.transform.GetChild(2).gameObject.SetActive(true);

        // write text onto lower display
        DisplayTextOnLowerBoard.setLowerText("Vergleiche...");

        // position loli and play animation
        vh.loli.transform.position = loliPosition[loliPos];
        vh.loli.transform.rotation = new Quaternion(0, loliRot, 0, 0);
        vh.loli.GetComponent<Animator>().SetBool("popUp", true);

        // wait one second
        yield return new WaitForSeconds(2f);

        vh.loli.GetComponent<Animator>().SetBool("popUp", false);

        // deactivate rings
        vh.ring1.transform.GetChild(2).gameObject.SetActive(false);
        vh.ring2.transform.GetChild(2).gameObject.SetActive(false);

        // activate green ring for larger child / red ring for smaller child
        vh.ring1.transform.GetChild(1).gameObject.SetActive(true);
        vh.ring2.transform.GetChild(0).gameObject.SetActive(true);

        // DisplayTextOnLowerBoard.setLowerText("Ein Tausch!");

        // loli vanishes again
        vh.loli.GetComponent<Animator>().SetBool("moveDown", true);
        yield return new WaitForSeconds(1f);
        vh.loli.GetComponent<Animator>().SetBool("moveDown", false);

        // deactivate the rings
        vh.ring1.transform.GetChild(1).gameObject.SetActive(false);
        vh.ring2.transform.GetChild(0).gameObject.SetActive(false);
    }

    // selects two elements with red ring -> no position change performed
    public static IEnumerator noSwitchNecessary(int a, int b, int loliPos, int loliRot) {

        // set position to children's position
        vh.ring1.transform.position = ringPosition[a];
        vh.ring1.transform.localScale = ringSize[a];
        vh.ring2.transform.position = ringPosition[b];
        vh.ring2.transform.localScale = ringSize[b];

        // put yellow ring around them
        vh.ring1.transform.GetChild(2).gameObject.SetActive(true);
        vh.ring2.transform.GetChild(2).gameObject.SetActive(true);

        // write text onto lower display
        DisplayTextOnLowerBoard.setLowerText("Vergleiche...");

        // position loli and play animation
        vh.loli.transform.position = loliPosition[loliPos];
        vh.loli.transform.rotation = new Quaternion(0, loliRot, 0, 0);
        vh.loli.GetComponent<Animator>().SetBool("popUp", true);
        yield return new WaitForSeconds(2f);
        vh.loli.GetComponent<Animator>().SetBool("popUp", false);

        // deactivate rings
        vh.ring1.transform.GetChild(2).gameObject.SetActive(false);
        vh.ring2.transform.GetChild(2).gameObject.SetActive(false);

        // put red ring around them
        vh.ring1.transform.GetChild(0).gameObject.SetActive(true);
        vh.ring2.transform.GetChild(0).gameObject.SetActive(true);

        DisplayTextOnLowerBoard.setLowerText("Kein Tausch!");

        // loli vanishes again
        vh.loli.GetComponent<Animator>().SetBool("moveDown", true);
        yield return new WaitForSeconds(1f);
        vh.loli.GetComponent<Animator>().SetBool("moveDown", false);

        // deactivate rings
        vh.ring1.transform.GetChild(0).gameObject.SetActive(false);
        vh.ring2.transform.GetChild(0).gameObject.SetActive(false);
    }

    public static IEnumerator destroySortedShips(int last) {

        if (last == 0) {
            //FindObjectOfType<AudioManager>().Play("applause");
            CallStatistics.callStatisticAfterVisualization();
        }

        // gets destroyed afterwards
        Destroy(shipsToSort[last], 0);
        yield return new WaitForSeconds(1f);
    }
}
