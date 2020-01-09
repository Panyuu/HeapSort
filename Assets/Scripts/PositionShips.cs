//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Visual.Base;

//namespace VisualFeedback {

//    public class PositionShips : MonoBehaviour {

//        int[] arrayToSort;

//        public PositionShips(int[] array) {

//            arrayToSort = array;
        
//        }

//        // Creates the heap structure according to the array.
//        public static IEnumerator positionShips(int[] arrayToSort) {

//            List<GameObject> ships = new List<GameObject>();

//            for (int i = 0; i < arrayToSort.Length; i++) {

//                Debug.Log("Visual Heap " + arrayToSort[i] + ", ");

//            }

//            for (int i = 0; i < arrayToSort.Length; i++) {

//                // Create Object Ship
//                ships.Add(Instantiate(VisualHeap.staticShipPrefab, VisualHeap.shipPosition[i], new Quaternion(0, 180, 0, 0)));
//                // sets the number on the sail
//                GameObject value = ships[i].transform.Find("Value").gameObject;
//                value.GetComponent<TextMesh>().text = arrayToSort[i].ToString();
//                // Rename Object
//                ships[i].name = "ShipIndex_" + i;


//                //anim[i] = ships[i].GetComponent<Animator>();

//                //anim[i].SetBool("isSurfacing", true);
//                yield return new WaitForSeconds(0.5f);
//                //anim[i].SetBool("isSurfacing", false);




//            }

//            VisualHeap.shipsToSort = ships.ToArray();
//        }
//    }

//}
