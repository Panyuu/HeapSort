using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHeap : MonoBehaviour
{

    public static VisualHeap vh;
    // Ship Prefab to instantiate
    [SerializeField] GameObject shipPrefab;  
    // positions of ships in heap
    static Vector3[] shipPosition;
    // stores information about the objects that are sorted
    static GameObject[] shipsToSort;
    // stores the position of the separated ship (currently not in heap)
    static Vector3 cachePosition;
    // cache
    static GameObject cacheObject;
    // animators of each object
    static Animator[] anim;

    private void Awake() {

        vh = this;
    }

    // Start is called before the first frame update
    void Start() { 

        // positions for the ships in heap-structure
        shipPosition = new Vector3[] { new Vector3(0, 2.2f, 2), 
            new Vector3(-6, 0.2f, 1), new Vector3(6, 0.2f, 1), new Vector3(-8, -1.8f, -0.5f), 
            new Vector3(-3, -1.8f, -0.5f), new Vector3(3, -1.8f, -0.5f), new Vector3(8, -1.8f, -0.5f) };
    }


    // Creates the heap structure according to the array.
    public static IEnumerator positionShips(int[] arrayToSort) {

        List<GameObject> ships = new List<GameObject>();

        for (int i = 0; i < arrayToSort.Length; i ++) {

            // Create Object Ship
            ships.Add(Instantiate(vh.shipPrefab, shipPosition[i], new Quaternion(0, 180, 0, 0)));

            // Rename Object
            ships[i].name = "ShipIndex_" + i;
            anim[i] = ships[i].GetComponent<Animator>();
            
            anim[i].SetBool("isSurfacing", true);
            yield return new WaitForSeconds(4.5f);
            anim[i].SetBool("isSurfacing", false);

            // sets the number on the sail
            GameObject value = ships[i].transform.Find("Value").gameObject;
            value.GetComponent<TextMesh>().text = arrayToSort[i].ToString();

        }

        shipsToSort = ships.ToArray();
        Debug.Log(shipsToSort.Length);

    }

    // changes complete ship object (position, rotation of sail, text on the ship)
    public static IEnumerator ChangeShipPosition(int a, int b) {

        Debug.Log(shipsToSort[a].transform.position);
        Debug.Log(shipsToSort[b].transform.position);

        // saves all necessary variables from a
        Vector3 positionShip = shipsToSort[a].transform.position;
        GameObject objectA = shipsToSort[a];
        //string textOnShip = shipsToSort[a].transform.Find("Value").gameObject.GetComponent<TextMesh>().text;

        // ships submerge
        anim[a].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        anim[a].SetBool("isSubmerging", false);
        anim[b].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        anim[b].SetBool("isSubmerging", false);

        // overwrites current values with the ones from other variable
        // saves values of b in a and changes index
        shipsToSort[a].transform.position = shipsToSort[b].transform.position;
        shipsToSort[a] = shipsToSort[b];

        // saves values of a in b and changes index
        shipsToSort[b].transform.position = positionShip;
        shipsToSort[b] = objectA;

        // lets ships surface on different position
        anim[a].SetBool("isSurfacing", true);
        yield return new WaitForSeconds(4.5f);
        anim[a].SetBool("isSurfacing", false);
        anim[b].SetBool("isSurfacing", true);
        yield return new WaitForSeconds(4.5f);
        anim[b].SetBool("isSurfacing", false);
    
    }

    // Separates last element from heap and switches position of root element to last element
    public static IEnumerator WriteRootToLast(int root, int lastElement) {

        Vector3 lastElementPosition = shipsToSort[lastElement].transform.position;

        // last element submerges
        anim[lastElement].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        anim[lastElement].SetBool("isSubmerging", false);

        // position of last element changed + not part of array anymore
        shipsToSort[lastElement].transform.position = cachePosition;
        cacheObject = shipsToSort[lastElement];

        // last element surfacing at cache position
        anim[lastElement].SetBool("isSurfacing", true);
        yield return new WaitForSeconds(4.5f);
        anim[lastElement].SetBool("isSurfacing", false);

        // root element is submerging
        anim[root].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        anim[root].SetBool("isSubmerging", false);

        // position of root element changed (index + ship position)
        shipsToSort[root].transform.position = lastElementPosition;
        shipsToSort[lastElement] = shipsToSort[root];

        // root is surfacing at position of lastElement
        anim[lastElement].SetBool("isSurfacing", true);
        yield return new WaitForSeconds(4.5f);
        anim[lastElement].SetBool("isSurfacing", false);

        // Element leaves the scene / screen
        anim[lastElement].SetBool("isMovingOutOfScene", true);
        yield return new WaitForSeconds(5);

        // gets destroyed afterwards
        Destroy(shipsToSort[lastElement], 0);
    }

    // moves largest child upwards to fill the heap back up
    public static IEnumerator moveUp(int parent, int child) {

        // child submerges
        anim[child].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        anim[child].SetBool("isSubmerging", false);

        // change position on screen and in array
        shipsToSort[child].transform.position = shipPosition[parent];
        shipsToSort[parent] = shipsToSort[child];

        // child surfaces at parents space
        anim[parent].SetBool("isSurfacing", true);
        yield return new WaitForSeconds(4.5f);
        anim[parent].SetBool("isSurfacing", false);
    }

    // writes the cache element back to the free space
    public static IEnumerator writeCacheBack(int free) {

        // cache Object is submerging
        cacheObject.GetComponent<Animator>().SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        cacheObject.GetComponent<Animator>().SetBool("isSubmerging", false);

        // change position of cacheObject back to free space in heap
        shipsToSort[free] = cacheObject;
        shipsToSort[free].transform.position = shipPosition[free];

        anim[free].SetBool("isSurfacing", true);
        yield return new WaitForSeconds(4.5f);
        anim[free].SetBool("isSurfacing", false);
    }

}
