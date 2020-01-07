using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHeap : MonoBehaviour
{

    public GameObject shipPrefab;
    Vector3[] shipPosition;
    Vector2[] backSailOptimization;
    GameObject[] shipsToSort;
    
    // Start is called before the first frame update
    void Start()
    {
        // positions for the ships in heap-structure
        shipPosition = new Vector3[] { new Vector3(0, 2, 2), 
            new Vector3(-6, 0, 1), new Vector3(6, 0, 1), new Vector3(-8, -1.6f, -0.5f), 
            new Vector3(-8, -1.6f, -0.5f), new Vector3(3, -1.6f, -0.5f), new Vector3(8, -1.6f, -0.5f)};

        // back sail needs repositioning according to the position of the ship in the scene. (Anja says it's ugly).
        backSailOptimization = new Vector2[] { new Vector2(-0.478665f, -24.536f), new Vector2(-0.7626685f, -49.536f),
            new Vector2(-0.2786685f, -16.536f), new Vector2(-1.078668f, -55.536f), new Vector2(-0.6786685f, -36.536f),
            new Vector2(-0.3786685f, -17.536f), new Vector2(-0.3786685f, -16.536f) };
    }


    // Creates the heap structure according to the array.
    public void positionShips(int[] arrayToSort) {


        List<GameObject> ships = new List<GameObject>();

        for (int i = 0; i < arrayToSort.Length; i ++) {

            // Create Object Ship
            ships.Add(Instantiate(shipPrefab, shipPosition[i], Quaternion.identity));

            // Rename Object
            ships[i].name = "ShipIndex_" + i;

            // changes rotation and position of backsail (depending on it's position)
            GameObject sail = ships[i].transform.Find("SegelHinten").gameObject;
            sail.transform.position = new Vector3(backSailOptimization[i].x, sail.transform.position.y, sail.transform.position.z);
            sail.transform.rotation = new Quaternion(backSailOptimization[i].y, sail.transform.rotation.y, sail.transform.rotation.z, sail.transform.rotation.w);

            // sets the number on the sail
            GameObject value = ships[i].transform.Find("Value").gameObject;
            value.GetComponent<TextMesh>().text = arrayToSort[i].ToString();

        }

        shipsToSort = ships.ToArray();

    }

    // changes complete ship object (position, rotation of sail, text on the ship)
    public void changeShipPosition(int a, int b) {


        // saves all necessary variables from a
        Vector3 positionShip = shipsToSort[a].transform.position;
        Vector3 positionSail = shipsToSort[a].transform.Find("SegelHinten").gameObject.transform.position;
        Quaternion rotationSail = shipsToSort[a].transform.Find("SegelHinten").gameObject.transform.rotation;
        string textOnShip = shipsToSort[a].transform.Find("Value").gameObject.GetComponent<TextMesh>().text;

        // overwrites current values with the ones from other variable
        // saves values of b in a
        shipsToSort[a].transform.position = shipsToSort[b].transform.position;
        shipsToSort[a].transform.Find("SegelHinten").gameObject.transform.position = shipsToSort[b].transform.Find("SegelHinten").gameObject.transform.position;
        shipsToSort[a].transform.Find("SegelHinten").gameObject.transform.rotation = shipsToSort[b].transform.Find("SegelHinten").gameObject.transform.rotation;
        shipsToSort[a].transform.Find("Value").gameObject.GetComponent<TextMesh>().text = shipsToSort[b].transform.Find("Value").gameObject.GetComponent<TextMesh>().text;

        // saves values of a in b
        shipsToSort[b].transform.position = positionShip;
        shipsToSort[b].transform.Find("SegelHinten").gameObject.transform.position = positionSail;
        shipsToSort[b].transform.Find("SegelHinten").gameObject.transform.rotation = rotationSail;
        shipsToSort[b].transform.Find("Value").gameObject.GetComponent<TextMesh>().text = textOnShip;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
