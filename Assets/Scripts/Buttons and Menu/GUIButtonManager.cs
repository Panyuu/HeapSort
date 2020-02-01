using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIButtonManager : MonoBehaviour {
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
    public void animationMaxStop() {
        MaxHeap.setPlayAnimation(false);

        startButton.SetActive(true);
        protoButton.SetActive(true);
        homeButton.SetActive(true);
        pauseButton.SetActive(false);
    }
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
}