using UnityEngine;
using UnityEngine.SceneManagement;


// author: Ibrahim Kirschstein
public class Menu : MonoBehaviour {
    public GameObject firstTutorial;

    /*
     * holds all functions for Buttons used in the main menu
     * 
     */

    //switches scenes to start the visualisation of the heapsort
    public void pressPlay() {
        SceneManager.LoadScene(1);
    }
    //closes application
    public void pressExit() {
        Application.Quit();
    }


    //sets the first tutorial GameObject active to make the tutorial visible
    public void startTutorial() {
        firstTutorial.SetActive(true);
    }
    //used for easier switches between tutorial parts and easier rearrangment
    public void nextCanvas(GameObject next) {
        next.SetActive(true);
        //now.SetActive(false);
    }
   

}
