using UnityEngine;
using UnityEngine.SceneManagement;


// author: Ibrahim Kirschstein
public class GUIButtonManager : MonoBehaviour {
   
    /*
     * holds all functions for Buttons used during the heapSort-Showcase
     */
    
    public GameObject pauseButton;
    public GameObject startButton;
    public GameObject protoButton;
    public GameObject homeButton;
    public GameObject Proto;

    private bool protoShown = false;

    public void Update() {

        if (Input.GetKeyDown("space")) {
            if (pauseButton.activeInHierarchy) {
                animationMaxStop();
            }
            else if (startButton.activeInHierarchy) {
                animationMaxStart();
            }
        }
    }
    //makes pausing/ unpausing the game with space key possible
    public void animationMaxStop() {
        MaxHeap.setPlayAnimation(false);

        startButton.SetActive(true);
        protoButton.SetActive(true);
        homeButton.SetActive(true);
        pauseButton.SetActive(false);
    }
    //Pauses Visualization and enables the other buttons
    public void animationMaxStart() {
        if (MaxHeap.startPossible) {
            MaxHeap.playAnimation = true;
            StartCoroutine(MaxHeap.startAnimation());

            pauseButton.SetActive(true);
            homeButton.SetActive(false);
            protoButton.SetActive(false);
            startButton.SetActive(false);
        }
    }
    //enables Visualization and disables the other buttons
    public void protocolShow() {
        if (protoShown) {
            Proto.SetActive(false);
            protoShown = false;
        }
        else {
            Proto.SetActive(true);
            protoShown = true;
        }
    }
    //activates the Protocol_Canvas
    public void returnHome() {
        //reset everything in every class
        MaxHeap.setBackEverything();
        GetNumberInput.setBackEverything();
        MaxHeapPlain.setBackEverything();
        GetValueForStatistic.setBackEverything();

        // reset everything in this class
        ManageArrayUI.prefabList.Clear();
        GetNumberInput.listForHeap.Clear();

        // load menu-scene
        SceneManager.LoadScene(0);
    }
    //resets the scene and switches back to the home menu
}