using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHeap : MonoBehaviour
{
    // singleton
    public static VisualHeap vh;


    // Ship Prefab to instantiate
    public GameObject shipPrefab;
    // positions of ships in heap
    static Vector3[] shipPosition;
    // animators of each object
    static Animator[] anim;
    // stores information about the objects that are sorted
    static GameObject[] shipsToSort;
    // set back rotation to default
    static Quaternion rotation;


    // stores the position of the separated ship (currently not in heap)
    static Vector3 cachePosition;
    // cache
    static GameObject cacheObject;
    //// animator from cache
    static Animator cacheAnimator;


    static int i;

    private void Awake() {

        vh = this;
        i = 0;
    }

    // Start is called before the first frame update
    void Start() {

        // positions for the ships in heap-structure
        shipPosition = new Vector3[] { new Vector3(0, -1f, 3),
            new Vector3(-6, -2.8f, 1), new Vector3(6, -2.8f, 1), new Vector3(-8, -4.8f, -0.5f),
            new Vector3(-3, -4.8f, -0.5f), new Vector3(3, -4.8f, -0.5f), new Vector3(8, -4.8f, -0.5f) };

        rotation = new Quaternion(0, 180, 0, 0);
        cachePosition = new Vector3(14, -1f, 2);
    }

    //public static void instantiateCacheObject(Vector3 position) {

    //    cachePosition = position;
    //    cacheObject = Instantiate(vh.shipPrefab, cachePosition, rotation);
    //    cacheObject.SetActive(false);
    //    cacheAnimator = cacheObject.GetComponent<Animator>();

    //}

    // Creates the heap structure according to the array.
    public static IEnumerator positionShips(int[] arrayToSort) {

        List<GameObject> ships = new List<GameObject>();
        List<Animator> animators = new List<Animator>();

        for (int i = 0; i < arrayToSort.Length; i ++) {

            Debug.Log("Schiff Ahoi");
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
        }

        //yield return new WaitForSeconds(0.5f);
        shipsToSort = ships.ToArray();
        anim = animators.ToArray();

    }

    // changes complete ship object (position, rotation of sail, text on the ship)
    public static IEnumerator ChangeShipPosition(int a, int b) {


        Debug.Log("Ships Position Changed");
        
        // saves all necessary variables from a
        GameObject objectA = shipsToSort[a];
        Animator animatorA = anim[a];
        //string textOnShip = shipsToSort[a].transform.Find("Value").gameObject.GetComponent<TextMesh>().text;

        // ships submerge
        anim[a].SetBool("isSubmerging", true);
        anim[b].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        anim[a].SetBool("isSubmerging", false);
        anim[b].SetBool("isSubmerging", false);
        //yield return new WaitForSeconds(5f);
        

        // overwrites current values with the ones from other variable
        // saves values of b in a and changes index
        shipsToSort[a].transform.rotation = rotation;
        anim[a] = anim[b];
        shipsToSort[a].transform.position = shipPosition[b];
        //anim[a] = shipsToSort[b].GetComponent<Animator>();
        shipsToSort[a] = shipsToSort[b];

        //Debug.Log("in Array gespeicherte Position: " + shipPosition[b].x + ", " + shipPosition[b].y +
        //    ", aktuelle Position: " + shipsToSort[a].transform.position.x + ", " + shipsToSort[b].transform.position.y);

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
        Debug.Log("Animation executed");




    }

    // Separates last element from heap and switches position of root element to last element
    public static IEnumerator WriteRootToLast(int root, int lastElement) {

        // last element submerges
        anim[lastElement].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        anim[lastElement].SetBool("isSubmerging", false);

        // write into cacheObject + adjust components
        shipsToSort[lastElement].transform.position = cachePosition;
        shipsToSort[lastElement].transform.rotation = rotation;
        cacheAnimator = anim[lastElement];
        cacheObject = shipsToSort[lastElement];
        

        // ship from last position now surfacing outside of heap, root element is submerging
        cacheAnimator.SetBool("isSurfacing", true);
        anim[root].SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
        cacheAnimator.SetBool("isSurfacing", false);
        anim[root].SetBool("isSubmerging", false);
        

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

        // Element leaves the scene / screen
        anim[lastElement].SetBool("isMovingOutOfScene", true);
        yield return new WaitForSeconds(5f);

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

        // cache Object is submerging
        cacheAnimator.SetBool("isSubmerging", true);
        yield return new WaitForSeconds(3.5f);
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

}
